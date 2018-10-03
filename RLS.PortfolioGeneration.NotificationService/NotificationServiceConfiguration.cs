namespace RLS.PortfolioGeneration.NotificationService
{
    public sealed class NotificationServiceConfiguration
    {
        public string OutputLogDirectory { get; set; }

        public string Auth0ClientId { get; set; }

        public string Auth0SecretKey { get; set; }

        public string Auth0Tenant { get; set; }
    }
}