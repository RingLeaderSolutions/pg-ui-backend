using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RLS.PortfolioGeneration.Persistence.Model;
using RLS.PortfolioGeneration.Persistence.Model.Clients;
using RLS.PortfolioGeneration.FrontendBackend.Dtos;

namespace RLS.PortfolioGeneration.FrontendBackend.Controllers
{
    [Route("api/[controller]")]
    public class MprnController : Controller
    {
        private readonly ModelDbContext _dbContext;

        public MprnController(ModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Mprn>> Get()
        {
            return await _dbContext.RetrieveAllMprns();
        }

        [HttpGet("portfolio/{portfolioId}")]
        public async Task<MprnsDto> GetByPortfolio(string portfolioId)
        {
            var mpans = (await _dbContext.RetrieveMprnsByPortfolio(portfolioId))
                .Select(Mapper.Map<MprnDto>).ToList();
            
            return new MprnsDto
            {
                Mprns = mpans
            };
        }

        [HttpGet("{id}")]
        public async Task<Mprn> Get(Guid id)
        {
            return await _dbContext.RetrieveMprnById(id);
        }

        [HttpPost]
        public async Task Post([FromBody]Mprn mpan)
        {
            await _dbContext.Add(mpan);
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody]Mprn mpan)
        {
            await _dbContext.Update(mpan);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _dbContext.DeleteMprn(id);
        }
    }
}