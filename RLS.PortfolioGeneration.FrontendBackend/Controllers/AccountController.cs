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
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ModelDbContext _dbContext;

        public AccountController(ModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<AccountDto>> Get()
        {
            var accounts = await _dbContext.RetrieveAllAccounts();
            
            return accounts.Select(Mapper.Map<AccountDto>)
                .ToList();
        }

        [HttpGet("{id}")]
        public async Task<AccountDto> Get(Guid id)
        {
            var account = await _dbContext.RetrieveAccountById(id);

            return Mapper.Map<AccountDto>(account);
        }

        [HttpPost]
        public async Task<CreatedResult> Post([FromBody]AccountDto accountDto)
        {
            var account = Mapper.Map<Account>(accountDto);

            var accountId = Guid.NewGuid();
            account.Id = accountId;

            await _dbContext.Add(account);

            return Created(new Uri($"/api/Account/{accountId}", UriKind.Relative), new { id = accountId });
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody]AccountDto accountDto)
        {
            var account = Mapper.Map<Account>(accountDto);
            account.Id = id;

            await _dbContext.Update(account);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _dbContext.DeleteAccount(id);
        }
    }
}
