using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RLS.PortfolioGeneration.FrontendBackend.Dtos.BulkUpload;
using RLS.PortfolioGeneration.Persistence.Model;
using RLS.PortfolioGeneration.Persistence.Model.Clients;


namespace RLS.PortfolioGeneration.FrontendBackend.Controllers
{
    [Route("api/bulk")]
    public class BulkUploadController : Controller
    {
        private readonly ModelDbContext _dbContext;

        public BulkUploadController(ModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<BulkResponseDto> Post([FromBody] BulkRequestDto bulkRequestDto)
        {
            var response = new BulkResponseDto();

            string accountState = BulkUploadResponseStates.Errored;
            var account = Mapper.Map<Account>(bulkRequestDto.Account);

            if (account.Id == Guid.Empty)
            {
                var accountId = Guid.NewGuid();
                account.Id = accountId;

                await _dbContext.Add(account);
                accountState = BulkUploadResponseStates.Created;
            }
            else
            {
                await _dbContext.Update(account);
                accountState = BulkUploadResponseStates.Updated;
            }

            response.Account = new BulkUploadResponseDto
            {
                Id = account.Id,
                State = accountState
            };

            foreach (var site in bulkRequestDto.Sites)
            {
                var siteResponse = new BulkUploadSitesResponseDto();
                var mpanResults = new List<BulkUploadResponseDto>();
                var mprnResults = new List<BulkUploadResponseDto>();
                foreach (var siteMpan in site.Mpans)
                {
                    var mpan = Mapper.Map<Mpan>(siteMpan);

                    var existing = await _dbContext.RetrieveMpanByCore(siteMpan.MpanCore);
                    var mpanId = existing?.Id ?? Guid.NewGuid();
                    mpan.Id = mpanId;

                    if (existing != null)
                    {
                        mpan.Id = 
                        await _dbContext.Update(mpan);
                    }
                }
            }

            return response;
        }
    }
}
