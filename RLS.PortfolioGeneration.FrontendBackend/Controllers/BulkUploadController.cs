using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RLS.PortfolioGeneration.FrontendBackend.Dtos;
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

        private ModelDbContext CreateDbContext()
        {
            var dbOptions = new DbContextOptionsBuilder<ModelDbContext>()
                .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                .Options;

            return new ModelDbContext(dbOptions);
        }


        private async Task<BulkUploadRequestValidationResult> ValidateBulkRequest(Guid accountId, BulkRequestDto requestDto)
        {
            var result = true;
            var errors = new List<ValidationError>();
            using (var dbContext = CreateDbContext())
            {
                var tenancies = await dbContext.RetrieveTenancyPeriodByAccount(accountId);
                foreach (var siteDto in requestDto.Sites)
                {
                    foreach (var mpanDto in siteDto.Mpans)
                    {
                        var existingMpan = await dbContext.RetrieveMpanByCore(mpanDto.MpanCore);
                        if (existingMpan == null) continue;
                        if (tenancies.SingleOrDefault(tp => tp.SiteId == existingMpan.Site.Id) != null) continue;

                        errors.Add(new ValidationError(ErrorEntity.Mpan, mpanDto.MpanCore, $"Unable to update MPAN as it is owned by a site which belongs to a different account to the one specified."));
                        result = false;
                    }

                    foreach (var mprnDto in siteDto.Mprns)
                    {
                        var existingMprn = await dbContext.RetrieveMprnByCore(mprnDto.MprnCore);
                        if (existingMprn == null) continue;
                        if (tenancies.SingleOrDefault(tp => tp.SiteId == existingMprn.Site.Id) != null) continue;

                        errors.Add(new ValidationError(ErrorEntity.Mprn, mprnDto.MprnCore, $"Unable to update MPRN as it is owned by a site which belongs to a different account to the one specified."));
                        result = false;
                    }

                    var existingSite = await dbContext.RetrieveSiteByCode(siteDto.SiteCode);

                    if (existingSite == null) continue;

                    // Attempt to retrieve a tenancy for this site
                    var siteTenancyPeriod = tenancies.SingleOrDefault(tp => tp.SiteId == existingSite.Id);

                    // If we find a tenancy period matching this site and account, it's valid
                    if (siteTenancyPeriod != null) continue;
                    errors.Add(new ValidationError(ErrorEntity.Site, siteDto.SiteCode, "Unable to update Site as no tenancy period relationship exists between the specified account and this site. This usually means it is already linked to another account."));
                    result = false;
                }
            }

            return new BulkUploadRequestValidationResult(result, errors);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] JObject obj)
        {
            var bulkRequestDto = obj.ToObject<BulkRequestDto>();
            var response = new BulkResponseDto();

            if (!Guid.TryParse(bulkRequestDto.AccountId, out var accountId))
            {
                return RespondWithBadRequest("AccountId was not in the correct format.");
            }

            using (var dbContext = CreateDbContext())
            {
                var account = await dbContext.RetrieveAccountById(accountId);
                if (account == null)
                {
                    return RespondWithBadRequest($"Specified account with id [{accountId.ToString()}] does not exist.");
                }
            }

            var validationResult = await ValidateBulkRequest(accountId, bulkRequestDto);
            if (!validationResult.IsValid)
            {
                return RespondWithBadRequest("Validation failed", validationResult.Errors.ToArray());
            }

            response.RequestId = bulkRequestDto.RequestId;
            response.PortfolioId = bulkRequestDto.PortfolioId;
            response.AccountId = bulkRequestDto.AccountId;

            var sites = new List<BulkUploadSitesResponseDto>();
            foreach (var site in bulkRequestDto.Sites)
            {
                BulkUploadSitesResponseDto siteResponse;
                using (var dbContext = CreateDbContext())
                {
                    siteResponse = await ProcessSite(dbContext, accountId, site);
                }

                using (var dbContext = CreateDbContext())
                {
                    var updatedSite = await dbContext.Sites
                        .AsQueryable()
                        .SingleOrDefaultAsync(s => s.Id == siteResponse.Id);

                    siteResponse.Mpans = await ProcessRequestMpans(dbContext, updatedSite, site.Mpans);
                    siteResponse.Mprns = await ProcessRequestMprns(dbContext, updatedSite, site.Mprns);
                }

                sites.Add(siteResponse);
            }

            response.Sites = sites.ToArray();
            return Ok(response);
        }

        private async Task<BulkUploadSitesResponseDto> ProcessSite(ModelDbContext dbContext, Guid accountId,
            QualifiedSiteDto siteDto)
        {

            var existingSite = await dbContext.RetrieveSiteByCode(siteDto.SiteCode);
            var siteExists = existingSite != null;

            var siteId = existingSite?.Id ?? Guid.NewGuid();
            
            string siteState;
            if (siteExists)
            {
                await SoftUpdateSiteFields(dbContext, existingSite, siteDto);
                await dbContext.Update(existingSite);

                siteState = BulkUploadResponseStates.Updated;
            }
            else
            {
                var mappedSite = Mapper.Map<Site>(siteDto);
                mappedSite.Id = siteId;

                await dbContext.Add(mappedSite);

                var tenancyPeriod = new TenancyPeriod
                {
                    Id = Guid.NewGuid(),
                    AccountId = accountId,
                    SiteId = siteId,
                    Site = mappedSite,
                    EffectiveFrom = DateTime.MinValue,
                    EffectiveTo = DateTime.MaxValue
                };

                await dbContext.Add(tenancyPeriod);
                siteState = BulkUploadResponseStates.Created;
            }

            return new BulkUploadSitesResponseDto
            {
                Id = siteId,
                SiteCode = siteDto.SiteCode,
                State = siteState
            };
        }

        private async Task<List<MeterBulkUploadResponseDto>> ProcessRequestMpans(ModelDbContext dbContext,
            Site associatedSite, MpanDto[] mpans)
        {
            var mpanResults = new List<MeterBulkUploadResponseDto>();
            foreach (var mpanDto in mpans)
            {
                var existing = await dbContext.RetrieveMpanByCore(mpanDto.MpanCore);
                var mpanId = existing?.Id ?? Guid.NewGuid();
                
                string mpanState;
                if (existing != null)
                {
                    await SoftUpdateMpanFields(dbContext, associatedSite, existing, mpanDto);
                    await dbContext.Update(existing);
                    mpanState = BulkUploadResponseStates.Updated;
                }
                else
                {
                    var mpan = Mapper.Map<Mpan>(mpanDto);
                    mpan.Id = mpanId;
                    mpan.Site = associatedSite;

                    await dbContext.Add(mpan);
                    mpanState = BulkUploadResponseStates.Created;
                }

                mpanResults.Add(new MeterBulkUploadResponseDto
                {
                    Core = mpanDto.MpanCore,
                    Id = mpanId,
                    State = mpanState
                });
            }

            return mpanResults;
        }

        private async Task<List<MeterBulkUploadResponseDto>> ProcessRequestMprns(ModelDbContext dbContext,
            Site associatedSite, MprnDto[] mprns)
        {
            var mprnResults = new List<MeterBulkUploadResponseDto>();
            {
                foreach (var mprnDto in mprns)
                {
                    var existing = await dbContext.RetrieveMprnByCore(mprnDto.MprnCore);
                    var mprnId = existing?.Id ?? Guid.NewGuid();
                    
                    string mprnState;
                    if (existing != null)
                    {
                        await SoftUpdateMprnFields(dbContext, associatedSite, existing, mprnDto);
                        await dbContext.Update(existing);
                        mprnState = BulkUploadResponseStates.Updated;
                    }
                    else
                    {
                        var mprn = Mapper.Map<Mprn>(mprnDto);

                        mprn.Id = mprnId;
                        mprn.Site = associatedSite;
                        await dbContext.Add(mprn);
                        mprnState = BulkUploadResponseStates.Created;
                    }

                    mprnResults.Add(new MeterBulkUploadResponseDto
                    {
                        Core = mprnDto.MprnCore,
                        Id = mprnId,
                        State = mprnState
                    });
                }

                return mprnResults;
            }
        }

        private async Task SoftUpdateSiteFields(ModelDbContext dbContext, Site existing, SiteDto updatedDto)
        {
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.Name, updatedDto.Name);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.Contact, updatedDto.Contact);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.Address1, updatedDto.Address1);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.Address2, updatedDto.Address2);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.Town, updatedDto.Town);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.CoT, updatedDto.CoT);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.BillingAddress, updatedDto.BillingAddress);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.Postcode, updatedDto.Postcode);

            await dbContext.Update(existing);
        }

        private async Task SoftUpdateMpanFields(ModelDbContext dbContext, Site newSite, Mpan existing, MpanDto updatedDto)
        {
            SetPropertyIfNotNull(existing, e => e.IsEnergized, updatedDto.IsEnergized);
            SetPropertyIfNotNull(existing, e => e.IsNewConnection, updatedDto.IsNewConnection);
            SetPropertyIfNotNull(existing, e => e.EAC, updatedDto.EAC);
            SetPropertyIfNotNull(existing, e => e.REC, updatedDto.REC);
            SetPropertyIfNotNull(existing, e => e.Capacity, updatedDto.Capacity);
            SetPropertyIfNotNull(existing, e => e.CCLEligible, updatedDto.CCLEligible);
            SetPropertyIfNotNull(existing, e => e.VATPercentage, updatedDto.VATPercentage);

            SetStringPropertyIfNotNullOrEmpty(existing, e => e.MeterType, updatedDto.MeterType);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.MeterTimeSwitchCode, updatedDto.MeterTimeSwitchCode);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.LLF, updatedDto.LLF);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.ProfileClass, updatedDto.ProfileClass);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.RetrievalMethod, updatedDto.RetrievalMethod);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.GSPGroup, updatedDto.GSPGroup);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.MeasurementClass, updatedDto.MeasurementClass);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.MOAgent, updatedDto.MOAgent);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.DAAgent, updatedDto.DAAgent);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.DCAgent, updatedDto.DCAgent);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.SerialNumber, updatedDto.SerialNumber);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.Voltage, updatedDto.Voltage);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.Postcode, updatedDto.Postcode);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.Connection, updatedDto.Connection);

            existing.TariffId = updatedDto.TariffId;
            existing.Site = newSite;

            await dbContext.Update(existing);
        }

        private async Task SoftUpdateMprnFields(ModelDbContext dbContext, Site newSite, Mprn existing, MprnDto updatedDto)
        {
            SetPropertyIfNotNull(existing, e => e.ChangeOfUse, updatedDto.ChangeOfUse);
            SetPropertyIfNotNull(existing, e => e.AQ, updatedDto.AQ);
            SetPropertyIfNotNull(existing, e => e.IsImperial, updatedDto.IsImperial);
            SetPropertyIfNotNull(existing, e => e.Size, updatedDto.Size);
            SetPropertyIfNotNull(existing, e => e.CCLEligible, updatedDto.CCLEligible);
            SetPropertyIfNotNull(existing, e => e.VATPercentage, updatedDto.VATPercentage);

            SetStringPropertyIfNotNullOrEmpty(existing, e => e.SerialNumber, updatedDto.SerialNumber);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.Make, updatedDto.Make);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.Model, updatedDto.Model);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.EmergencyContactAddress, updatedDto.EmergencyContactAddress);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.EmergencyContactName, updatedDto.EmergencyContactName);
            SetStringPropertyIfNotNullOrEmpty(existing, e => e.EmergencyContactTelephone, updatedDto.EmergencyContactTelephone);

            existing.TariffId = updatedDto.TariffId;
            existing.Site = newSite;

            await dbContext.Update(existing);
        }

        private void SetStringPropertyIfNotNullOrEmpty<T>(T obj, Expression<Func<T, string>> selector, string val)
        {
            if (string.IsNullOrWhiteSpace(val)) return;
            SetProperty(obj, selector, val);
        }

        private void SetPropertyIfNotNull<T, K>(T obj, Expression<Func<T, K>> selector, K? val)
            where K : struct, IEquatable<K>
        {
            if (!val.HasValue) return;
            SetProperty(obj, selector, val.Value);
        }

        private void SetProperty<T, K>(T obj, Expression<Func<T, K>> selector, K val)
            where K : IEquatable<K>
        {
            var memberExpression = (MemberExpression)selector.Body;
            var property = (PropertyInfo)memberExpression.Member;

            var currentValue = (K) property.GetValue(obj);

            if (currentValue == null || !currentValue.Equals(val))
            {
                property.SetValue(obj, val);
            }
        }

        private BadRequestObjectResult RespondWithBadRequest(string errorMessage, ValidationError[] errors = null)
        {
            var result = new
            {
                state = errorMessage,
                errors = errors ?? new ValidationError[0]
            };

            return BadRequest(result);
        }

        public class BulkUploadRequestValidationResult
        {
            public BulkUploadRequestValidationResult(bool isValid, List<ValidationError> errors)
            {
                IsValid = isValid;
                Errors = errors;
            }

            public bool IsValid { get; }

            public List<ValidationError> Errors { get; }
        }

        public class ValidationError
        {
            public ValidationError(ErrorEntity entityType, string name, string reason)
            {
                EntityType = entityType;
                Name = name;
                Reason = reason;
            }

            public ErrorEntity EntityType { get;  }

            public string Name { get; }

            public string Reason { get; }
        }

        public enum ErrorEntity
        {
            Site,
            Mpan,
            Mprn
        }
    }
}
