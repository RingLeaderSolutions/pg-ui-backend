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
        public DbSet<Mprn> Mprns { get; set; }
        
        public async Task<List<Mprn>> RetrieveAllMprns()
        {
            return
                await RetrieveMprns()
                    .ToListAsync();
        }

        public async Task<Mprn> RetrieveMprnById(Guid id)
        {
            return
                await RetrieveMprns().SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Mprn> RetrieveMprnByCore(string mprnCore)
        {
            return
                await RetrieveMprns().SingleOrDefaultAsync(a => a.MprnCore == mprnCore);
        }

        public async Task Add(Mprn mprn)
        {
            Mprns.Add(mprn);
            await SaveChangesAsync();
        }

        public async Task Update(Mprn mprn)
        {
            Mprns.Attach(mprn);

            var entry = Entry(mprn);
            entry.State = EntityState.Modified;

            await SaveChangesAsync();
        }

        public async Task DeleteMprn(Guid id)
        {
            var mprn = new Mprn { Id = id };
            Mprns.Attach(mprn);
            Mprns.Remove(mprn);
            await SaveChangesAsync();
        }

        private IQueryable<Mprn> RetrieveMprns()
        {
            return Mprns
                .AsQueryable()
                .AsNoTracking();
        }
    }
}
