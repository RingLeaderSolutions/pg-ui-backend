using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class PortfolioDto
    {
        public string Id { get; set; }

        public DateTime? ContractStart { get; set; }

        public DateTime? ContractEnd { get; set; }

        public virtual List<PortfolioMeterDto> PortfolioMeters { get; set; }
    }
}