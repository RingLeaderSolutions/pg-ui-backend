using System;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class SiteDto
    {
        public Guid? Id { get; set; }

        public string SiteCode { get; set; }

        public string Name { get; set; }

        public string Contact { get; set; }

        public string Address { get; set; }

        public string CoT { get; set; }

        public string BillingAddress { get; set; }

        public string Postcode { get; set; }
    }

    public class SiteWithMetersDto : SiteDto
    {
        public MpanDto[] Mpans { get; set; }

        public MprnDto[] Mprns { get; set; }
    }
}