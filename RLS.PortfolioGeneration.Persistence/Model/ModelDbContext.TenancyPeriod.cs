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
        public DbSet<TenancyPeriod> TenancyPeriods { get; set; }
        
        public async Task<List<TenancyPeriod>> RetrieveAllTenancyPeriods()
        {
            return
                await RetrieveTenancyPeriods()
                    .ToListAsync();
        }

        public async Task<List<TenancyPeriod>> RetrieveTenancyPeriodByAccount(Guid accountId)
        {
            return
                await RetrieveTenancyPeriods().Where(tp => tp.AccountId == accountId)
                    .ToListAsync();
        }

        public async Task<TenancyPeriod> RetrieveTenancyPeriodById(Guid id)
        {
            return
                await RetrieveTenancyPeriods()
                .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task Add(TenancyPeriod tenancyPeriod)
        {
            TenancyPeriods.Add(tenancyPeriod);
            await SaveChangesAsync();
        }

        public async Task Update(TenancyPeriod tenancyPeriod)
        {
            TenancyPeriods.Attach(tenancyPeriod);

            var entry = Entry(tenancyPeriod);
            entry.State = EntityState.Modified;

            await SaveChangesAsync();
        }

        public async Task DeleteTenancyPeriod(Guid id)
        {
            var tenancyPeriod = new TenancyPeriod { Id = id };
            TenancyPeriods.Attach(tenancyPeriod);
            TenancyPeriods.Remove(tenancyPeriod);
            await SaveChangesAsync();
        }

        private IQueryable<TenancyPeriod> RetrieveTenancyPeriods()
        {
            return TenancyPeriods.AsQueryable().AsNoTracking();
        }
    }
}