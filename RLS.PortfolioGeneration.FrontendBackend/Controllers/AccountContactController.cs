using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RLS.PortfolioGeneration.FrontendBackend.Dtos;
using RLS.PortfolioGeneration.Persistence.Model;
using RLS.PortfolioGeneration.Persistence.Model.Clients;

namespace RLS.PortfolioGeneration.FrontendBackend.Controllers
{
    [Route("api/Contact")]
    public class AccountContactController : Controller
    {
        private readonly ModelDbContext _dbContext;

        public AccountContactController(ModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<AccountContactDto>> Get()
        {
            var contacts = await _dbContext.RetrieveAllContacts();

            return contacts.Select(Mapper.Map<AccountContactDto>)
                .ToList();
        }

        [HttpGet("account/{accountId}")]
        public async Task<AccountContactDto[]> GetByAccount(Guid accountId)
        {
            var accountContacts = (await _dbContext.RetrieveContactsByAccountId(accountId))
                .Select(Mapper.Map<AccountContactDto>);

            return accountContacts.ToArray();
        }

        [HttpGet("{id}")]
        public async Task<AccountContactDto> Get(Guid id)
        {
            var contact = await _dbContext.RetrieveContactById(id);

            return Mapper.Map<AccountContactDto>(contact);
        }

        [HttpPost]
        public async Task<CreatedResult> Post([FromBody]AccountContactDto contactDto)
        {
            var contact = Mapper.Map<AccountContact>(contactDto);

            var contactId = Guid.NewGuid();
            contact.Id = contactId;

            await _dbContext.Add(contact);

            return Created(new Uri($"/api/Contact/{contactId}", UriKind.Relative), new { id = contactId });
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody]AccountContactDto contactDto)
        {
            var contact = Mapper.Map<AccountContact>(contactDto);
            contact.Id = id;

            await _dbContext.Update(contact);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _dbContext.DeleteContact(id);
        }
    }
}