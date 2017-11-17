using System;
using Newtonsoft.Json;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class PortfolioMeterDto
    {
        public string Id { get; set; }

        public string MeterNumber { get; set; }

        public string MeterType { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        [JsonIgnore]
        public virtual PortfolioDto Portfolio { get; set; }
    }
}