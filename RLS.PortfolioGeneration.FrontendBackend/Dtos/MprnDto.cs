using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class MprnDto
    {
        public Guid? Id { get; set; }

        [Required]
        public string MprnCore { get; set; }

        public string SerialNumber { get; set; }

        public bool? IsImperial { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public double? AQ { get; set; }

        public bool? ChangeOfUse { get; set; }

        public decimal? Size { get; set; }
        
        public decimal VATPercentage { get; set; }

        public bool CCLEligible { get; set; }
        
        public string EmergencyContactAddress { get; set; }

        public string EmergencyContactName { get; set; }

        public string EmergencyContactTelephone { get; set; }

        public Guid? TariffId { get; set; }

        public bool IsAMR { get; set; }
    }

    public class MprnsDto
    {
        [JsonProperty("mprns")]
        public List<MprnDto> Mprns { get; set; }
    }
}