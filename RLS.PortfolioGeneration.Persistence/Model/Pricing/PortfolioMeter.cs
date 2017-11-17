using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLS.PortfolioGeneration.Persistence.Model.Pricing
{
    [Table("portfolio_meter", Schema = "pricing")]
    public class PortfolioMeter
    {
        [Key, Column("id")]
        public string Id { get; set; }

        [Key, Column("meterNumber")]
        public string MeterNumber { get; set; }

        [Column("effectiveFrom")]
        public DateTime? EffectiveFrom { get; set; }

        [Column("effectiveTo")]
        public DateTime? EffectiveTo { get; set; }
        
        [Column("meterType")]
        public string MeterType { get; set; }

        [ForeignKey("portfolioId")]
        public virtual Portfolio Portfolio { get; set; }
    }
}