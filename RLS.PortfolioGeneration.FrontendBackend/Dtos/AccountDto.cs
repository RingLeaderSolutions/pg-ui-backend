using System;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class AccountDto
    {
        public Guid Id { get; set; }

        public string AccountNumber { get; set; }

        public string Name { get; set; }

        public string CompanyRegistrationNumber { get; set; }

        public string Address { get; set; }

        public string Contact { get; set; }

        public string CompanyStatus { get; set; }

        public string RegistrationAddress { get; set; }

        public string CreditRating { get; set; }
    }
}