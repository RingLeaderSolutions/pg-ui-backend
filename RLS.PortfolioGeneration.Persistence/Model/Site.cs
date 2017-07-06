using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLS.PortfolioGeneration.Persistence.Model
{
    [Table("site")]
    public class Site
    {
        [Key, Column("Id")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Contact { get; set; }

        public decimal Capacity { get; set; }

        public string Address { get; set; }

        public string CoT { get; set; }

        public virtual Account Account { get; set; }

        public virtual ICollection<Mpan> Mpans { get; set; }
    }
}