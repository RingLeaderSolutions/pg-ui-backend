using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLS.PortfolioGeneration.Persistence.Model.Clients
{
    [Table("account")]
    public class Account
    {
        [Key, Column("Id")]
        public Guid Id { get; set; }

        public string AccountNumber { get; set; }

        public string Name { get; set; }

        public string CompanyRegistrationNumber { get; set; }

        public string Address { get; set; }

        public string Contact { get; set; }

        public string CompanyStatus { get; set; }

        public string RegistrationAddress { get; set; }

        public string CreditRating { get; set; }

        public virtual ICollection<TenancyPeriod> TenancyPeriods { get; set; }
    }
}
