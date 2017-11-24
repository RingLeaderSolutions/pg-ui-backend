using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLS.PortfolioGeneration.Persistence.Model.Clients
{
    [Table("mprn")]
    public class Mprn
    {
        [Key, Column("Id")]
        public Guid Id { get; set; }

        [Required]
        public string MprnCore { get; set; }

        public string SerialNumber { get; set; }

        public bool IsImperial { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public double AQ { get; set; }

        public bool ChangeOfUse { get; set; }

        public decimal Size { get; set; }

        public virtual Site Site { get; set; }
    }
}