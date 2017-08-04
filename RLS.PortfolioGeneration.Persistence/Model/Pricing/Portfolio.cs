using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLS.PortfolioGeneration.Persistence.Model.Pricing
{
    [Table("portfolio", Schema = "pricing")]
    public class Portfolio
    {
        [Key, Column("id")]
        public string Id { get; set; }

        [Column("contractStart")]
        public DateTime? ContractStart { get; set; }

        [Column("contractEnd")]
        public DateTime? ContractEnd { get; set; }

        public virtual ICollection<PortfolioMpan> PortfolioMpans { get; set; }
    }
}