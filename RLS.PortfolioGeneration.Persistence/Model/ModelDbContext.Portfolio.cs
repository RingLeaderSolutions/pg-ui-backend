using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RLS.PortfolioGeneration.Persistence.Extensions;
using RLS.PortfolioGeneration.Persistence.Model.Clients;
using RLS.PortfolioGeneration.Persistence.Model.Pricing;

namespace RLS.PortfolioGeneration.Persistence.Model
{
    public partial class ModelDbContext
    {
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioMeter> PortfolioMeters { get; set; }

        public async Task<Portfolio> RetrievePortfolioById(string id)
        {
            return
                await RetrievePortfolios().SingleOrDefaultAsync(a => a.Id == id);
        }

        private IQueryable<Portfolio> RetrievePortfolios()
        {
            return Portfolios.AsQueryable().AsNoTracking();
        }

        public async Task<List<Mpan>> RetrieveMpansByPortfolio(string portfolioId)
        {
            var tenancyPeriods = GetTenancyPeriods();
            var portfolioMeters = GetPortfolioMeters(portfolioId);

            return
                (await (from pm in portfolioMeters
                        join mpan in Mpans on pm.MeterNumber equals mpan.MpanCore
                        join tp in tenancyPeriods on mpan.Site.Id equals tp.SiteId
                        where tp.EffectiveFrom <= pm.Portfolio.ContractStart
                              && tp.EffectiveTo >= pm.Portfolio.ContractEnd
                        select tp
                    )
                    .ToListAsync())
                    .SelectMany(tp => tp.Site.Mpans).ToList();
        }

        public async Task<List<Mprn>> RetrieveMprnsByPortfolio(string portfolioId)
        {
            var tenancyPeriods = GetTenancyPeriods();
            var portfolioMeters = GetPortfolioMeters(portfolioId);

            return
                (await (from pm in portfolioMeters
                        join mprn in Mprns on pm.MeterNumber equals mprn.MprnCore
                        join tp in tenancyPeriods on mprn.Site.Id equals tp.SiteId
                        where tp.EffectiveFrom <= pm.Portfolio.ContractStart
                              && tp.EffectiveTo >= pm.Portfolio.ContractEnd
                        select tp
                    )
                    .ToListAsync())
                .SelectMany(tp => tp.Site.Mprns).ToList();
        }
        
        public async Task<List<Site>> RetrieveSiteAndMetersForPortfolio(string portfolioId)
        {
            var tenancyPeriods = GetTenancyPeriods();
            var portfolioMeters = GetPortfolioMeters(portfolioId);

            var mpanSites =  await (from pm in portfolioMeters
                         from site in Sites
                         join mpan in Mpans on pm.MeterNumber equals mpan.MpanCore
                         join tp in tenancyPeriods on mpan.Site.Id equals tp.SiteId
                         where tp.EffectiveFrom <= pm.EffectiveFrom
                               && tp.EffectiveTo >= pm.EffectiveTo
                         where site.Id == mpan.Site.Id
                         select site)
                .Include(s => s.Mpans)
                .Include(s => s.Mprns)
                .AsNoTracking()
                .ToListAsync();

            var mprnSites = await (from pm in portfolioMeters
                    from site in Sites
                    join mprn in Mprns on pm.MeterNumber equals mprn.MprnCore
                    join tp in tenancyPeriods on mprn.Site.Id equals tp.SiteId
                    where tp.EffectiveFrom <= pm.EffectiveFrom
                          && tp.EffectiveTo >= pm.EffectiveTo
                    where site.Id == mprn.Site.Id
                    select site)
                .Include(s => s.Mpans)
                .Include(s => s.Mprns)
                .AsNoTracking()
                .ToListAsync();

            return mpanSites.Union(mprnSites)
                .DistinctBy(s => s.Id)
                .ToList();
        }

        private IQueryable<PortfolioMeter> GetPortfolioMeters(string portfolioId)
        {
            return from p in Portfolios.AsQueryable().Include(p => p.PortfolioMeters)
                where p.Id == portfolioId
                from pm in p.PortfolioMeters
                select pm;
        }

        private IIncludableQueryable<TenancyPeriod, ICollection<Mpan>> GetTenancyPeriods()
        {
            return TenancyPeriods.AsQueryable()
                .Include(tp => tp.Account)
                .Include(tp => tp.Site)
                .ThenInclude(s => s.Mpans);
        }
    }
}