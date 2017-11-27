using System;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class TenancyPeriodDto
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Guid SiteId { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }
    }
}