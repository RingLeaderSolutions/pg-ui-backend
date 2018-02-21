using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLS.PortfolioGeneration.Persistence.Model.Clients
{
    [Table("site")]
    public class Site
    {
        [Required, Column("Id")]
        public Guid Id { get; set; }

        [Required]
        public string SiteCode { get; set; }

        public string Name { get; set; }

        public string Contact { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Town { get; set; }

        public string CoT { get; set; }

        public string BillingAddress { get; set; }

        public string Postcode { get; set; }

        public virtual ICollection<TenancyPeriod> TenancyPeriods { get; set; }

        public ICollection<Mpan> Mpans { get; set; }

        public ICollection<Mprn> Mprns { get; set; }
    }
}