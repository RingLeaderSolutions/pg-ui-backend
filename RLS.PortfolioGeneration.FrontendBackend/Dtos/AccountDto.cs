using System;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class AccountDto
    {
        public Guid? Id { get; set; }

        public string AccountNumber { get; set; }

        public string CompanyName { get; set; }

        public string CompanyRegistrationNumber { get; set; }

        public string Address { get; set; }

        public string Postcode { get; set; }

        public string CountryOfOrigin { get; set; }

        public DateTime? IncorporationDate { get; set; }

        public string Contact { get; set; }

        public string CompanyStatus { get; set; }

        public string CreditRating { get; set; }

        public bool IsRegisteredCharity { get; set; }

        public bool HasCCLException { get; set; }

        public bool IsVATEligible { get; set; }

        public bool HasFiTException { get; set; }
    }
}