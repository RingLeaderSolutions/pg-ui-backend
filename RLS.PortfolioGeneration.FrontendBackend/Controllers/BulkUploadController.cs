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
        public async Task<ActionResult> Post([FromBody] BulkRequestDto bulkRequestDto)
        {
            var response = new BulkResponseDto();

            if (bulkRequestDto.Account.Id == Guid.Empty)
            {
                return BadRequest("No account id provided.");
            }
            
            var account = Mapper.Map<Account>(bulkRequestDto.Account);
            await _dbContext.Update(account);
            
            response.Account = new BulkUploadResponseDto
            {
                Id = account.Id,
                State = BulkUploadResponseStates.Updated
            };

            foreach (var site in bulkRequestDto.Sites)
            {
                var mappedSite = Mapper.Map<Account>(bulkRequestDto.Account);

                var existingSite = await _dbContext.RetrieveSiteByCode(site.SiteCode);
                var siteId = existingSite?.Id ?? Guid.NewGuid();

                mappedSite.Id = siteId;

                string siteState;
                if (existingSite != null)
                {
                    await _dbContext.Update(mappedSite);
                    siteState = BulkUploadResponseStates.Updated;
                }
                else
                {
                    await _dbContext.Add(mappedSite);
                    siteState = BulkUploadResponseStates.Created;
                }

                var siteResponse = new BulkUploadSitesResponseDto
                {
                    Id = siteId,
                    SiteCode = site.SiteCode,
                    State =  siteState
                };

                var mpanResults = new List<MeterBulkUploadResponseDto>();
                foreach (var siteMpan in site.Mpans)
                {
                    var mpan = Mapper.Map<Mpan>(siteMpan);

                    var existing = await _dbContext.RetrieveMpanByCore(siteMpan.MpanCore);

                    var mpanId = existing?.Id ?? Guid.NewGuid();
                    mpan.Id = mpanId;

                    string mpanState;
                    if (existing != null)
                    {
                        await _dbContext.Update(mpan);
                        mpanState = BulkUploadResponseStates.Updated;
                    }
                    else
                    {
                        await _dbContext.Add(mpan);
                        mpanState = BulkUploadResponseStates.Created;
                    }

                    mpanResults.Add(new MeterBulkUploadResponseDto
                    {
                        Id = mpanId,
                        State = mpanState
                    });
                }

                var mprnResults = new List<MeterBulkUploadResponseDto>();
                foreach (var siteMprn in site.Mprns)
                {
                    var mprn = Mapper.Map<Mprn>(siteMprn);

                    var existing = await _dbContext.RetrieveMprnByCore(siteMprn.MprnCore);
                    var mprnId = existing?.Id ?? Guid.NewGuid();
                    mprn.Id = mprnId;

                    string mprnState;
                    if (existing != null)
                    {
                        await _dbContext.Update(mprn);
                        mprnState = BulkUploadResponseStates.Updated;
                    }
                    else
                    {
                        await _dbContext.Add(mprn);
                        mprnState = BulkUploadResponseStates.Created;
                    }

                    mprnResults.Add(new MeterBulkUploadResponseDto
                    {
                        Id = mprnId,
                        State = mprnState
                    });
                }

                siteResponse.Mpans = mpanResults;
                siteResponse.Mprns = mprnResults;

                response.Sites = siteResponse;
            }

            return Ok(response);
        }
    }
}
