using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RLS.PortfolioGeneration.Persistence.Model;
using RLS.PortfolioGeneration.Persistence.Model.Clients;

namespace RLS.PortfolioGeneration.FrontendBackend.Controllers
{
    [Route("api/[controller]")]
    public class TenancyPeriodController : Controller
    {
        private readonly ModelDbContext _dbContext;

        public TenancyPeriodController(ModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<TenancyPeriod>> Get()
        {
            return await _dbContext.RetrieveAllTenancyPeriods();
        }

        [HttpGet("{id}")]
        public async Task<TenancyPeriod> Get(Guid id)
        {
            return await _dbContext.RetrieveTenacyPeriodById(id);
        }

        [HttpPost]
        public async Task Post([FromBody]TenancyPeriod mpan)
        {
            await _dbContext.Add(mpan);
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody]TenancyPeriod tenancyPeriod)
        {
            await _dbContext.Update(tenancyPeriod);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _dbContext.DeleteTenancyPeriod(id);
        }
    }
}