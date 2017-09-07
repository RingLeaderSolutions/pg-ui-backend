using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class MpanDto
    {
        public Guid Id { get; set; }

        public string MpanCore { get; set; }

        public virtual SiteDto Site { get; set; }
    }

    public class MpansDto
    {
        [JsonProperty("mpans")]
        public List<MpanDto> Mpans { get; set; }
    }
}