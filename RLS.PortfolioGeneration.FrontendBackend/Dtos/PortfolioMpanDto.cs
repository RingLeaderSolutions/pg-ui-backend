using System;
using Newtonsoft.Json;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class PortfolioMpanDto
    {
        public string Id { get; set; }

        public string MpanCore { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        [JsonIgnore]
        public virtual PortfolioDto Portfolio { get; set; }
    }
}