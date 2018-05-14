using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLS.PortfolioGeneration.Persistence.Model.Clients
{
    [Table("contact")]
    public class AccountContact
    {
        [Key, Column("Id")]
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }

        public virtual Account Account { get; set; }
    }
}