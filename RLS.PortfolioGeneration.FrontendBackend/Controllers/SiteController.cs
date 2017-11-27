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
    public class SiteController : Controller
    {
        private readonly ModelDbContext _dbContext;

        public SiteController(ModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<SiteDto>> Get()
        {
            var sites = await _dbContext.RetrieveAllSites();

            return sites.Select(Mapper.Map<SiteDto>)
                .ToList();
        }

        [HttpGet("{id}")]
        public async Task<SiteDto> Get(Guid id)
        {
            var site = await _dbContext.RetrieveSiteById(id);

            return Mapper.Map<SiteDto>(site);
        }

        [HttpPost]
        public async Task<CreatedResult> Post([FromBody]SiteDto siteDto)
        {
            var site = Mapper.Map<Site>(siteDto);

            var siteId = Guid.NewGuid();
            site.Id = siteId;

            await _dbContext.Add(site);

            return Created(new Uri($"/api/Site/{siteId}", UriKind.Relative), new { id = siteId });
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody]SiteDto siteDto)
        {
            var site = Mapper.Map<Site>(siteDto);
            site.Id = id;

            await _dbContext.Update(site);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _dbContext.DeleteSite(id);
        }
    }
}