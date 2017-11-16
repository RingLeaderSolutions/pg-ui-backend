using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class MprnDto
    {
        public Guid Id { get; set; }

        public string MprnCore { get; set; }

        public string SerialNumber { get; set; }

        public bool IsImperial { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public double AQ { get; set; }

        public bool ChangeOfUse { get; set; }

        public string Size { get; set; }

        public virtual SiteDto Site { get; set; }
    }

    public class MprnsDto
    {
        [JsonProperty("mprns")]
        public List<MprnDto> Mprns { get; set; }
    }
}