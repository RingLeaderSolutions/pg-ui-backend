using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class MpanDto
    {
        public Guid? Id { get; set; }

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

        public string Postcode { get; set; }

        public bool IsNewConnection { get; set; }

        public string Connection { get; set; }

        public int EAC { get; set; }

        public int REC { get; set; }

        public decimal Capacity { get; set; }
    }

    public class MpansDto
    {
        [JsonProperty("mpans")]
        public List<MpanDto> Mpans { get; set; }
    }
}