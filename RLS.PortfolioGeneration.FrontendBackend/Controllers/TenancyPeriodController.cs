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
    public class TenancyPeriodController : Controller
    {
        private readonly ModelDbContext _dbContext;

        public TenancyPeriodController(ModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<TenancyPeriodDto>> Get()
        {
            var tenancyPeriods = await _dbContext.RetrieveAllTenancyPeriods();

            return tenancyPeriods.Select(Mapper.Map<TenancyPeriodDto>)
                .ToList();
        }

        [HttpGet("{id}")]
        public async Task<TenancyPeriodDto> Get(Guid id)
        {
            var tenancyPeriod = await _dbContext.RetrieveTenancyPeriodById(id);

            return Mapper.Map<TenancyPeriodDto>(tenancyPeriod);
        }

        [HttpPost]
        public async Task<CreatedResult> Post([FromBody]TenancyPeriodDto tenancyPeriodDto)
        {
            var tenancyPeriod = Mapper.Map<TenancyPeriod>(tenancyPeriodDto);

            var tenancyId = Guid.NewGuid();
            tenancyPeriod.Id = tenancyId;
            
            await _dbContext.Add(tenancyPeriod);

            return Created(new Uri($"/api/TenancyPeriod/{tenancyId}", UriKind.Relative), new { id = tenancyId });
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody]TenancyPeriodDto tenancyPeriodDto)
        {
            var tenancyPeriod = Mapper.Map<TenancyPeriod>(tenancyPeriodDto);
            tenancyPeriod.Id = id;

            await _dbContext.Update(tenancyPeriod);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _dbContext.DeleteTenancyPeriod(id);
        }
    }
}