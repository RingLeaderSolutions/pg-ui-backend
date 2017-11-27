using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RLS.PortfolioGeneration.Persistence.Model.Clients;

namespace RLS.PortfolioGeneration.Persistence.Model
{
    public partial class ModelDbContext
    {
        public DbSet<Site> Sites { get; set; }


        public async Task<List<Site>> RetrieveAllSites()
        {
            return
                await RetrieveSites()
                    .ToListAsync();
        }

        public async Task<Site> RetrieveSiteById(Guid id)
        {
            return
                await RetrieveSites()
                    .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Site> RetrieveSiteByCode(string code)
        {
            return
                await RetrieveSites()
                    .SingleOrDefaultAsync(a => a.SiteCode == code);
        }

        public async Task Add(Site site)
        {
            Sites.Add(site);
            await SaveChangesAsync();
        }

        public async Task Update(Site site)
        {
            Sites.Attach(site);

            var entry = Entry(site);
            entry.State = EntityState.Modified;

            await SaveChangesAsync();
        }

        public async Task DeleteSite(Guid id)
        {
            var site = new Site { Id = id };
            Sites.Attach(site);
            Sites.Remove(site);
            await SaveChangesAsync();
        }

        private IQueryable<Site> RetrieveSites()
        {
            return Sites.AsQueryable().AsNoTracking();
        }
    }
}
