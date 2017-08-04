using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLS.PortfolioGeneration.Persistence.Model.Clients
{
    [Table("tenancy_period")]
    public class TenancyPeriod
    {
        [Key, Column("id")]
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Guid SiteId { get; set; }

        public virtual Account Account { get; set; }

        public virtual Site Site { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }
    }
}