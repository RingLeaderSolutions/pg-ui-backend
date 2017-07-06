using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RLS.PortfolioGeneration.Persistence.Model;


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
        public async Task<IEnumerable<Account>> Get()
        {
            return await _dbContext.RetrieveAllAccounts();
        }

        [HttpGet("{id}")]
        public async Task<Account> Get(Guid id)
        {
            return await _dbContext.RetrieveAccountById(id);
        }

        [HttpPost]
        public async Task Post([FromBody]Account account)
        {
            await _dbContext.Add(account);
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody]Account account)
        {
            await _dbContext.Update(account);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _dbContext.DeleteAccount(id);
        }
    }
}
