﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<AccountDto>> GetAccounts([FromQuery(Name = "search")] string searchTerm = "")
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var results = await _dbContext.SearchAccountNames(searchTerm);

                return results.Select(Mapper.Map<AccountDto>).ToList();
            }
            
            var accounts = await _dbContext.RetrieveAllAccounts();

            return accounts.Select(Mapper.Map<AccountDto>)
                .ToList();
        }
        
        /// <summary>
        /// Attempts to retrieve an account with the specified id.
        /// </summary>
        /// <param name="id">The id of the account to retrieve</param>
        [ProducesResponseType(typeof(AccountDto), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAccountById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Provided AccountId was not in a valid format.");
            }

            var account = await _dbContext.RetrieveAccountById(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<AccountDto>(account));
        }

        /// <summary>
        /// Attempts to retrieve account(s) with the specified name.
        /// </summary>
        /// <param name="name">The name of the account(s) to retrieve</param>
        [ProducesResponseType(typeof(AccountDto), 200)]
        [ProducesResponseType(typeof(AccountDto[]), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(404)]
        [HttpGet("name/{name}")]
        public async Task<ActionResult> Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var accounts = await _dbContext.RetrieveAccountsByName(name);
            if (accounts.Count == 0)
            {
                return NotFound();
            }

            var mappedAccounts = accounts.Select(Mapper.Map<AccountDto>)
                .ToList();

            return mappedAccounts.Count > 1 ? Ok(mappedAccounts[0]) : Ok(mappedAccounts);
        }

        /// <summary>
        /// Attempts to retrieve the full account tree including its tenancy periods, sites and meters.
        /// </summary>
        /// <param name="id">The id of the account to retrieve</param>
        [ProducesResponseType(typeof(AccountWithSitesDto), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(404)]
        [HttpGet("detail/{id}")]
        public async Task<ActionResult> GetTree(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Provided AccountId was not in a valid format.");
            }

            var account = await _dbContext.Accounts
                .Include(a => a.TenancyPeriods)
                    .ThenInclude(tp => tp.Site)
                        .ThenInclude(s => s.Mpans)
                .Include(a => a.TenancyPeriods)
                    .ThenInclude(tp => tp.Site)
                        .ThenInclude(s => s.Mprns)
                .Include(a => a.Contacts)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (account == null)
            {
                return NotFound();
            }

            var accountDto = Mapper.Map<AccountWithSitesDto>(account);

            accountDto.Sites = account.TenancyPeriods
                .Where(tp => tp.AccountId == id)
                .OrderBy(tp => tp.Site.SiteCode)
                .Select(tp =>
                {
                    var siteDto = Mapper.Map<SiteWithTenancyDto>(tp.Site);
                    siteDto.TenancyStart = tp.EffectiveFrom;
                    siteDto.TenancyEnd = tp.EffectiveTo;
                    return siteDto;
                })
                .ToArray();

            accountDto.Contacts = account.Contacts.Select(Mapper.Map<AccountContactDto>)
                .ToArray();

            return Ok(accountDto);
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
