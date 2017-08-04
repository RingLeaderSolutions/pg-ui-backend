using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLS.PortfolioGeneration.Persistence.Model.Pricing
{
    [Table("portfolio_mpan", Schema = "pricing")]
    public class PortfolioMpan
    {
        [Key, Column("id")]
        public string Id { get; set; }

        [Key, Column("mpanCore")]
        public string MpanCore { get; set; }

        [Column("effectiveFrom")]
        public DateTime? EffectiveFrom { get; set; }

        [Column("effectiveTo")]
        public DateTime? EffectiveTo { get; set; }

        [ForeignKey("portfolioId")]
        public virtual Portfolio Portfolio { get; set; }
    }
}