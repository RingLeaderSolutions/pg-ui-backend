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
    public class MpanController : Controller
    {
        private readonly ModelDbContext _dbContext;

        public MpanController(ModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Mpan>> Get()
        {
            return await _dbContext.RetrieveAllMpans();
        }

        [HttpGet("portfolio/{portfolioId}")]
        public async Task<IEnumerable<MpanDto>> GetByPortfolio(string portfolioId)
        {
            return (await _dbContext.RetrieveMpansByPortfolio(portfolioId))
                .Select(Mapper.Map<MpanDto>);
        }

        [HttpGet("{id}")]
        public async Task<Mpan> Get(Guid id)
        {
            return await _dbContext.RetrieveMpanById(id);
        }

        [HttpPost]
        public async Task Post([FromBody]Mpan mpan)
        {
            await _dbContext.Add(mpan);
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody]Mpan mpan)
        {
            await _dbContext.Update(mpan);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _dbContext.DeleteMpan(id);
        }
    }
}