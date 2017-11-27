using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RLS.PortfolioGeneration.FrontendBackend.Dtos.BulkUpload;
using RLS.PortfolioGeneration.Persistence.Model;
using RLS.PortfolioGeneration.Persistence.Model.Clients;


namespace RLS.PortfolioGeneration.FrontendBackend.Controllers
{
    [Route("api/bulk")]
    public class BulkUploadController : Controller
    {
        private readonly IConfiguration _configuration;

        public BulkUploadController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ModelDbContext CreateDbContext()
        {
            var dbOptions = new DbContextOptionsBuilder<ModelDbContext>()
                .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                .Options;

            return new ModelDbContext(dbOptions);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BulkRequestDto bulkRequestDto)
        {
            var response = new BulkResponseDto();
            response.RequestId = bulkRequestDto.RequestId;
            response.PortfolioId = bulkRequestDto.PortfolioId;

            var accountId = bulkRequestDto.Account.Id;
            if (accountId == null || accountId == Guid.Empty)
            {
                return BadRequest("No account id provided.");
            }
            
            var account = Mapper.Map<Account>(bulkRequestDto.Account);
            using (var dbContext = CreateDbContext())
            {
                await dbContext.Update(account);
            }
            
            response.Account = new BulkUploadResponseDto
            {
                Id = accountId.Value,
                State = BulkUploadResponseStates.Updated
            };

            foreach (var site in bulkRequestDto.Sites)
            {
                var mappedSite = Mapper.Map<Site>(site);

                Guid siteId;
                string siteState;
                using (var dbContext = CreateDbContext())
                {
                    var existingSite = await dbContext.RetrieveSiteByCode(site.SiteCode);
                    var siteExists = existingSite != null;

                    siteId = existingSite?.Id ?? Guid.NewGuid();

                    mappedSite.Id = siteId;

                    if (siteExists)
                    {
                        await dbContext.Update(mappedSite);
                        siteState = BulkUploadResponseStates.Updated;
                    }
                    else
                    {
                        await dbContext.Add(mappedSite);

                        var tenancyPeriod = new TenancyPeriod
                        {
                            Id = Guid.NewGuid(),
                            AccountId = accountId.Value,
                            SiteId = siteId,
                            Site = mappedSite,
                            EffectiveFrom = DateTime.MinValue,
                            EffectiveTo = DateTime.MaxValue
                        };

                        await dbContext.Add(tenancyPeriod);
                        siteState = BulkUploadResponseStates.Created;
                    }
                }

                var siteResponse = new BulkUploadSitesResponseDto
                {
                    Id = siteId,
                    SiteCode = site.SiteCode,
                    State =  siteState
                };

                using (var dbContext = CreateDbContext())
                {
                    var updatedSite = await dbContext.Sites
                        .AsQueryable()
                        .SingleOrDefaultAsync(s => s.Id == siteId);

                    var mpanResults = new List<MeterBulkUploadResponseDto>();
                    foreach (var siteMpan in site.Mpans)
                    {
                        var mpan = Mapper.Map<Mpan>(siteMpan);

                        var existing = await dbContext.RetrieveMpanByCore(siteMpan.MpanCore);

                        var mpanId = existing?.Id ?? Guid.NewGuid();
                        mpan.Id = mpanId;
                        mpan.Site = updatedSite;

                        string mpanState;
                        if (existing != null)
                        {
                            await dbContext.Update(mpan);
                            mpanState = BulkUploadResponseStates.Updated;
                        }
                        else
                        {
                            await dbContext.Add(mpan);
                            mpanState = BulkUploadResponseStates.Created;
                        }

                        mpanResults.Add(new MeterBulkUploadResponseDto
                        {
                            Core = mpan.MpanCore,
                            Id = mpanId,
                            State = mpanState
                        });
                    }

                    var mprnResults = new List<MeterBulkUploadResponseDto>();
                    foreach (var siteMprn in site.Mprns)
                    {
                        var mprn = Mapper.Map<Mprn>(siteMprn);

                        var existing = await dbContext.RetrieveMprnByCore(siteMprn.MprnCore);
                        var mprnId = existing?.Id ?? Guid.NewGuid();
                        mprn.Id = mprnId;
                        mprn.Site = updatedSite;

                        string mprnState;
                        if (existing != null)
                        {
                            await dbContext.Update(mprn);
                            mprnState = BulkUploadResponseStates.Updated;
                        }
                        else
                        {
                            await dbContext.Add(mprn);
                            mprnState = BulkUploadResponseStates.Created;
                        }

                        mprnResults.Add(new MeterBulkUploadResponseDto
                        {
                            Core = mprn.MprnCore,
                            Id = mprnId,
                            State = mprnState
                        });
                    }

                    siteResponse.Mpans = mpanResults;
                    siteResponse.Mprns = mprnResults;
                }
                
                response.Sites = siteResponse;
            }
            
            return Ok(response);
        }
    }
}
