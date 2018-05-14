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
        public DbSet<AccountContact> Contacts { get; set; }

        public async Task<List<AccountContact>> RetrieveContactsByAccountId(Guid accountId)
        {
            return
                await RetrieveContacts()
                    .Where(ac => ac.AccountId == accountId)
                    .ToListAsync();
        }

        public async Task<List<AccountContact>> RetrieveAllContacts()
        {
            return
                await RetrieveContacts()
                    .ToListAsync();
        }

        public async Task<AccountContact> RetrieveContactById(Guid id)
        {
            return
                await RetrieveContacts()
                    .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task Add(AccountContact contact)
        {
            Contacts.Add(contact);
            await SaveChangesAsync();
        }

        public async Task Update(AccountContact contact)
        {
            Contacts.Attach(contact);

            var entry = Entry(contact);
            entry.State = EntityState.Modified;

            await SaveChangesAsync();
        }

        public async Task DeleteContact(Guid id)
        {
            var contact = new AccountContact { Id = id };
            Contacts.Attach(contact);
            Contacts.Remove(contact);
            await SaveChangesAsync();
        }

        private IQueryable<AccountContact> RetrieveContacts()
        {
            return Contacts
                .AsQueryable()
                .AsNoTracking();
        }
    }
}
