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

        public string CompanyName { get; set; }

        public string CompanyRegistrationNumber { get; set; }

        public string Address { get; set; }

        public string Postcode { get; set; }

        public string CountryOfOrigin { get; set; }

        public DateTime? IncorporationDate { get; set; }

        public string Contact { get; set; }

        public string CompanyStatus { get; set; }

        public string CreditRating { get; set; }

        public bool IsRegisteredCharity { get; set; }

        public bool HasCCLException { get; set; }

        public bool IsVATEligible { get; set; }

        public bool HasFiTException { get; set; }

        public virtual ICollection<TenancyPeriod> TenancyPeriods { get; set; }
    }
}
