using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RLS.PortfolioGeneration.FrontendBackend.Dtos;
using RLS.PortfolioGeneration.FrontendBackend.Services;
using RLS.PortfolioGeneration.Persistence.Model;
using RLS.PortfolioGeneration.Persistence.Model.Clients;

namespace RLS.PortfolioGeneration.FrontendBackend.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ModelDbContext _dbContext;
        private AccountReportingService _reportingService;

        public AccountController(ModelDbContext dbContext, IOptions<HierarchyServiceConfiguration> configuration)
        {
            _dbContext = dbContext;

            this._reportingService = new AccountReportingService(configuration.Value);
        }

        /// <summary>
        /// Retrieves all registered accounts.
        /// </summary>
        /// <returns>All of the accounts.</returns>
        [ProducesResponseType(typeof(List<AccountDto>), 200)]
        [HttpGet]
        public async Task<List<AccountDto>> Get()
        {
            var accounts = await _dbContext.RetrieveAllAccounts();
            
            return accounts.Select(Mapper.Map<AccountDto>)
                .ToList();
        }


        /// <summary>
        /// Attempts to retrieve an account with the specified id.
        /// </summary>
        /// <param name="id">The id of the account to retrieve</param>
        [ProducesResponseType(typeof(AccountDto), 200)]
        [HttpGet("{id}")]
        public async Task<AccountDto> Get(Guid id)
        {
            var account = await _dbContext.RetrieveAccountById(id);

            return Mapper.Map<AccountDto>(account);
        }

        /// <summary>
        /// Attempts to retrieve the full account tree including its tenancy periods, sites and meters.
        /// </summary>
        /// <param name="id">The id of the account to retrieve</param>
        [ProducesResponseType(typeof(AccountWithSitesDto), 200)]
        [HttpGet("detail/{id}")]
        public async Task<AccountWithSitesDto> GetTree(Guid id)
        {
            var account = await _dbContext.RetrieveAccountById(id);

            var sites = new List<SiteWithTenancyDto>();
            var tenancyPeriods = await _dbContext.RetrieveTenancyPeriodByAccount(id);

            foreach (var tenancyPeriod in tenancyPeriods)
            {
                var site = await _dbContext.RetrieveSiteTreeById(tenancyPeriod.SiteId);
                sites.Add(MapSiteTree(site, tenancyPeriod));
            }

            var accountContacts = await _dbContext.RetrieveContactsByAccountId(id);

            var accountDto = Mapper.Map<AccountWithSitesDto>(account);
            accountDto.Sites = sites.ToArray();
            accountDto.Contacts = accountContacts
                .Select(Mapper.Map<AccountContactDto>)
                .ToArray();

            return accountDto;
        }

        private SiteWithTenancyDto MapSiteTree(Site site, TenancyPeriod tenancyPeriod)
        {
            var siteTree = Mapper.Map<SiteWithTenancyDto>(site);
            siteTree.TenancyStart = tenancyPeriod.EffectiveFrom;
            siteTree.TenancyEnd = tenancyPeriod.EffectiveTo;

            return siteTree;
        }

        /// <summary>
        /// Creates an account.
        /// </summary>
        /// <param name="accountDto">The DTO representing the account to create.</param>
        /// <returns>The id of the created account.</returns>
        [ProducesResponseType(201)]
        [HttpPost]
        public async Task<CreatedResult> Post([FromBody]AccountDto accountDto)
        {
            var account = Mapper.Map<Account>(accountDto);

            var accountId = Guid.NewGuid();
            account.Id = accountId;

            await _dbContext.Add(account);

            await _reportingService.ReportAccountCreated(accountId.ToString());
            return Created(new Uri($"/api/Account/{accountId}", UriKind.Relative), new { id = accountId });
        }

        /// <summary>
        /// Updates the specified account.
        /// </summary>
        /// <param name="id">The id of the account to update.</param>
        /// <param name="accountDto">The representation of the account for which to update.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task Put(Guid id, [FromBody]AccountDto accountDto)
        {
            var account = Mapper.Map<Account>(accountDto);
            account.Id = id;
            
            await _dbContext.Update(account);
            await _reportingService.ReportAccountChanged(id.ToString());
        }

        /// <summary>
        /// Updates the status related flags for the given account.
        /// </summary>
        /// <param name="id">The id of the account to update</param>
        /// <param name="flagsDto">The representation of company status flags (CCL Exceptions, Registered charity, etc)</param>
        /// <returns>The updated account object.</returns>
        [HttpPut("status/{id}")]
        [ProducesResponseType(200)]
        public async Task<AccountDto> Put(Guid id, [FromBody] CompanyFlagsDto flagsDto)
        {
            var account = await _dbContext.RetrieveAccountById(id);

            account.HasCCLException = flagsDto.HasCCLException;
            account.HasFiTException = flagsDto.HasFiTException;
            account.IsRegisteredCharity = flagsDto.IsRegisteredCharity;
            account.IsVATEligible = flagsDto.IsVATEligible;

            await _dbContext.Update(account);

            await _reportingService.ReportAccountChanged(id.ToString());
            
            var updatedAccount = await _dbContext.RetrieveAccountById(id);

            return Mapper.Map<AccountDto>(updatedAccount);
        }

        /// <summary>
        /// Deletes the account with the specified id.
        /// </summary>
        /// <param name="id">The id of the account to delete.</param>
        [ProducesResponseType(200)]
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _dbContext.DeleteAccount(id);
            await _reportingService.ReportAccountDeleted(id.ToString());
        }
    }
}
