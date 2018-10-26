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
        public DbSet<Account> Accounts { get; set; }

        public async Task<List<Account>> RetrieveAllAccounts()
        {
            return
                await RetrieveAccounts()
                    .ToListAsync();
        }
        
        public async Task<Account> RetrieveAccountById(Guid id)
        {
            return
                await RetrieveAccounts()
                    .SingleOrDefaultAsync(a => a.Id == id);
        }
        
        public async Task<List<Account>> RetrieveAccountsByName(string name)
        {
            return
                await Accounts
                    .Where(a => a.CompanyName == name)
                    .ToListAsync();
        }

        public async Task<List<Account>> SearchAccountNames(string searchTerm)
        {
            return
                await Accounts
                    .Where(ac => ac.CompanyName.Contains(searchTerm))
                    .ToListAsync();
        }

        public async Task Add(Account account)
        {
            Accounts.Add(account);
            await SaveChangesAsync();
        }

        public async Task Update(Account account)
        {
            Accounts.Attach(account);

            var entry = Entry(account);
            entry.State = EntityState.Modified;

            await SaveChangesAsync();
        }

        public async Task DeleteAccount(Guid id)
        {
            var account = new Account { Id = id };
            Accounts.Attach(account);
            Accounts.Remove(account);
            await SaveChangesAsync();
        }

        private IQueryable<Account> RetrieveAccounts()
        {
            return Accounts
                .AsQueryable()
                .AsNoTracking();
        }
    }
}
