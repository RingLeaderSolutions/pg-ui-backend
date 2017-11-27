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

        /// <summary>
        /// Retrieves all registered accounts.
        /// </summary>
        /// <returns>All of the accounts.</returns>
        [ProducesResponseType(typeof(List<AccountDto>), 200)]
        [HttpGet]
        public async Task<List<AccountDto>> Get()
        {
            var accounts = await _dbContext.RetrieveAllAccounts();
            
            return accounts.Select(Mapper.Map<AccountDto>)
                .ToList();
        }


        /// <summary>
        /// Attempts to retrieve an account with the specified id.
        /// </summary>
        /// <param name="id">The id of the account to retrieve</param>
        [ProducesResponseType(typeof(AccountDto), 201)]
        [HttpGet("{id}")]
        public async Task<AccountDto> Get(Guid id)
        {
            var account = await _dbContext.RetrieveAccountById(id);

            return Mapper.Map<AccountDto>(account);
        }

        /// <summary>
        /// Creates an account.
        /// </summary>
        /// <param name="accountDto">The DTO representing the account to create.</param>
        /// <returns>The id of the created account.</returns>
        [ProducesResponseType(201)]
        [HttpPost]
        public async Task<CreatedResult> Post([FromBody]AccountDto accountDto)
        {
            var account = Mapper.Map<Account>(accountDto);

            var accountId = Guid.NewGuid();
            account.Id = accountId;

            await _dbContext.Add(account);

            return Created(new Uri($"/api/Account/{accountId}", UriKind.Relative), new { id = accountId });
        }

        /// <summary>
        /// Updates the specified account.
        /// </summary>
        /// <param name="id">The id of the account to update.</param>
        /// <param name="accountDto">The representation of the account for which to update.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task Put(Guid id, [FromBody]AccountDto accountDto)
        {
            var account = Mapper.Map<Account>(accountDto);
            account.Id = id;

            await _dbContext.Update(account);
        }

        /// <summary>
        /// Deletes the account with the specified id.
        /// </summary>
        /// <param name="id">The id of the account to delete.</param>
        [ProducesResponseType(200)]
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _dbContext.DeleteAccount(id);
        }
    }
}
