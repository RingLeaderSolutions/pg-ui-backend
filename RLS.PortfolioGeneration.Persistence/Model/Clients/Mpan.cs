using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLS.PortfolioGeneration.Persistence.Model.Clients
{
    [Table("mpan")]
    public class Mpan
    {
        [Key, Column("Id")]
        public Guid Id { get; set; }

        [Required]
        public string MpanCore { get; set; }

        public string EnergisationStatus { get; set; }

        public string MeterOperatorMpid { get; set; }

        public string DataAggregatorMpid { get; set; }

        public string DataCollectorMpid { get; set; }

        public virtual Site Site { get; set; }
    }
}