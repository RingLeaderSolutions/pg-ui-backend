using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RLS.PortfolioGeneration.Persistence.Model;

namespace RLS.PortfolioGeneration.FrontendBackend.Controllers
{
    [Route("api/[controller]")]
    public class SiteController : Controller
    {
        private readonly ModelDbContext _dbContext;

        public SiteController(ModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Site>> Get()
        {
            return await _dbContext.RetrieveAllSites();
        }

        [HttpGet("{id}")]
        public async Task<Site> Get(Guid id)
        {
            return await _dbContext.RetrieveSiteById(id);
        }

        [HttpPost]
        public async void Post([FromBody]Site site)
        {
            await _dbContext.Add(site);
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody]Site site)
        {
            await _dbContext.Update(site);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _dbContext.DeleteSite(id);
        }
    }
}