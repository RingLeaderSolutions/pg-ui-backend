using System;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class AccountContactDto
    {
        public Guid? Id { get; set; }

        public Guid AccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }
    }
}