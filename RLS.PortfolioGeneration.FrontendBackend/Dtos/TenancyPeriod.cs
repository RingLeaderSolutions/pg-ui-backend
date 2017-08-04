using System;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class TenancyPeriodDto
    {
        public Guid Id { get; set; }

        public virtual AccountDto Account { get; set; }
        
        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }
    }
}