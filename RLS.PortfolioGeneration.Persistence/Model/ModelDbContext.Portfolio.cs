using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RLS.PortfolioGeneration.Persistence.Model.Clients;
using RLS.PortfolioGeneration.Persistence.Model.Pricing;

namespace RLS.PortfolioGeneration.Persistence.Model
{
    public partial class ModelDbContext
    {
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioMpan> PortfolioMpans { get; set; }

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
            var tenancyPeriods = TenancyPeriods.AsQueryable()
                            .Include(tp => tp.Account)
                            .Include(tp => tp.Site)
                            .ThenInclude(s => s.Mpans);
            return
                (await (from p in Portfolios.AsQueryable().Include(p => p.PortfolioMpans)
                        where p.Id == portfolioId
                        from pm in p.PortfolioMpans
                        where pm.EffectiveFrom >= p.ContractStart
                              && pm.EffectiveTo <= p.ContractEnd
                        join mpan in Mpans on pm.MpanCore equals mpan.MpanCore
                        join tp in tenancyPeriods on mpan.Site.Id equals tp.SiteId
                        where tp.EffectiveFrom >= pm.Portfolio.ContractStart
                              && tp.EffectiveTo <= pm.Portfolio.ContractEnd
                        select tp
                    )
                    .ToListAsync())
                    .SelectMany(tp => tp.Site.Mpans).ToList();
        }
    }
}