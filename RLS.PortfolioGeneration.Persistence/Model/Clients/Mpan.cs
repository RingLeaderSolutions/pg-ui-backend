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

        public string MeterType { get; set; }

        public string MeterTimeSwitchCode { get; set; }

        public string LLF { get; set; }

        public string ProfileClass { get; set; }

        public string RetrievalMethod { get; set; }

        public string GSPGroup { get; set; }

        public string MeasurementClass { get; set; }

        public bool IsEnergized { get; set; }

        public string MOAgent { get; set; }

        public string DAAgent { get; set; }

        public string DCAgent { get; set; }

        public string SerialNumber { get; set; }

        public string Voltage { get; set; }

        public string Postcode { get; set; }

        public bool IsNewConnection { get; set; }

        public string Connection { get; set; }

        public int EAC { get; set; }

        public int REC { get; set; }

        public decimal Capacity { get; set; }

        public decimal VATPercentage { get; set; }

        public bool CCLEligible { get; set; }

        public Guid? TariffId { get; set; }

        public virtual Site Site { get; set; }
    }
}