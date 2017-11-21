using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class SiteDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Contact { get; set; }

        public string Address { get; set; }

        public string CoT { get; set; }

        public virtual List<TenancyPeriodDto> TenancyPeriods { get; set; }
    }
}