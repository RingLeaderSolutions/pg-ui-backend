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
        public async Task<List<MpanDto>> Get()
        {
            var mpans = await _dbContext.RetrieveAllMpans();

            return mpans.Select(Mapper.Map<MpanDto>)
                .ToList();
        }

        [HttpGet("portfolio/{portfolioId}")]
        public async Task<MpansDto> GetByPortfolio(string portfolioId)
        {
            var mpans = (await _dbContext.RetrieveMpansByPortfolio(portfolioId))
                .Select(Mapper.Map<MpanDto>).ToList();
            
            return new MpansDto
            {
                Mpans = mpans
            };
        }

        [HttpGet("{id}")]
        public async Task<MpanDto> Get(Guid id)
        {
            var mpan = await _dbContext.RetrieveMpanById(id);

            return Mapper.Map<MpanDto>(mpan);
        }

        [HttpPost]
        public async Task<CreatedResult> Post([FromBody]MpanDto mpanDto)
        {
            var mpan = Mapper.Map<Mpan>(mpanDto);

            var mpanId = Guid.NewGuid();
            mpan.Id = mpanId;
            
            await _dbContext.Add(mpan);

            return Created(new Uri($"/api/Mpan/{mpanId}", UriKind.Relative), new { id = mpanId });
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody]MpanDto mpanDto)
        {
            var mpan = Mapper.Map<Mpan>(mpanDto);
            mpan.Id = id;

            await _dbContext.Update(mpan);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _dbContext.DeleteMpan(id);
        }
    }
}