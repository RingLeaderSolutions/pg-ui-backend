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
        public DbSet<Mpnr> Mpnrs { get; set; }


        public async Task<List<Mpnr>> RetrieveAllMpnrs()
        {
            return
                await RetrieveMpnrs()
                    .ToListAsync();
        }

        public async Task<Mpnr> RetrieveMpnrById(Guid id)
        {
            return
                await RetrieveMpnrs().SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task Add(Mpnr mpnr)
        {
            Mpnrs.Add(mpnr);
            await SaveChangesAsync();
        }

        public async Task Update(Mpnr mpnr)
        {
            Mpnrs.Attach(mpnr);
            await SaveChangesAsync();
        }

        public async Task DeleteMpnr(Guid id)
        {
            var mpnr = new Mpnr { Id = id };
            Mpnrs.Attach(mpnr);
            Mpnrs.Remove(mpnr);
            await SaveChangesAsync();
        }

        private IQueryable<Mpnr> RetrieveMpnrs()
        {
            return Mpnrs
                .AsQueryable()
                .AsNoTracking();
        }
    }
}
