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
        public async Task<List<MprnDto>> Get()
        {
            var mprns = await _dbContext.RetrieveAllMprns();

            return mprns.Select(Mapper.Map<MprnDto>)
                .ToList();
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
        public async Task<MprnDto> Get(Guid id)
        {
            var mprn = await _dbContext.RetrieveMprnById(id);

            return Mapper.Map<MprnDto>(mprn);
        }

        [HttpPost]
        public async Task<CreatedResult> Post([FromBody]MprnDto mprnDto)
        {
            var mprn = Mapper.Map<Mprn>(mprnDto);

            var mprnId = Guid.NewGuid();
            mprn.Id = mprnId;

            await _dbContext.Add(mprn);

            return Created(new Uri($"/api/Mprn/{mprnId}", UriKind.Relative), new { id = mprnId });
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody]MprnDto mprnDto)
        {
            var mprn = Mapper.Map<Mprn>(mprnDto);
            mprn.Id = id;

            await _dbContext.Update(mprn);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _dbContext.DeleteMprn(id);
        }
    }
}