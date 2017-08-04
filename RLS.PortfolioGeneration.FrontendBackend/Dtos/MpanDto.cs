using System;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class MpanDto
    {
        public Guid Id { get; set; }

        public string MpanCore { get; set; }

        public virtual SiteDto Site { get; set; }
    }
}