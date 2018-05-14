using System;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class SiteDto
    {
        public Guid? Id { get; set; }

        public string SiteCode { get; set; }

        public string Name { get; set; }

        public string Contact { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Town { get; set; }

        public string CoT { get; set; }

        public string BillingAddress { get; set; }

        public string Postcode { get; set; }
    }

    public class SiteWithMetersDto : SiteDto
    {
        public MpanDto[] Mpans { get; set; }

        public MprnDto[] Mprns { get; set; }
    }

    public class SiteWithTenancyDto : SiteWithMetersDto
    {
        public DateTime TenancyStart { get; set; }

        public DateTime TenancyEnd { get; set; }
    }
}