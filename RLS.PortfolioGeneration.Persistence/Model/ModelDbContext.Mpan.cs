using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RLS.PortfolioGeneration.Persistence.Model
{
    public partial class ModelDbContext
    {
        public DbSet<Mpan> Mpans { get; set; }


        public async Task<List<Mpan>> RetrieveAllMpans()
        {
            return
                await RetrieveMpans()
                    .ToListAsync();
        }

        public async Task<Mpan> RetrieveMpanById(Guid id)
        {
            return
                await RetrieveMpans().SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task Add(Mpan mpan)
        {
            Mpans.Add(mpan);
            await SaveChangesAsync();
        }

        public async Task Update(Mpan mpan)
        {
            Mpans.Attach(mpan);
            await SaveChangesAsync();
        }

        public async Task DeleteMpan(Guid id)
        {
            var mpan = new Mpan { Id = id };
            Mpans.Attach(mpan);
            Mpans.Remove(mpan);
            await SaveChangesAsync();
        }

        private IQueryable<Mpan> RetrieveMpans()
        {
            return Mpans.AsQueryable().AsNoTracking();
        }
    }
}
