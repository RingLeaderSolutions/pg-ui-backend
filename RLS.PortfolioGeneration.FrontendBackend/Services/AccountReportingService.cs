using System.Threading.Tasks;
using Flurl.Http;

namespace RLS.PortfolioGeneration.FrontendBackend.Services
{
    public sealed class AccountReportingService
    {
        private readonly HierarchyServiceConfiguration _configuration;

        public AccountReportingService(HierarchyServiceConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task ReportAccountCreated(string accountId)
        {
            var message = new NotificationMessage("created", "account created", "account", accountId, null, null);
            await MakeApiRequest(message);
        }

        public async Task ReportAccountDeleted(string accountId)
        {
            var message = new NotificationMessage("deleted", "account deleted", "account", accountId, null, null);
            await MakeApiRequest(message);
        }

        public async Task ReportAccountChanged(string accountId)
        {
            var message = new NotificationMessage("updated", "account updated", "account", accountId, null, null);
            await MakeApiRequest(message);
        }

        private async Task MakeApiRequest(object postData)
        {
            var fullUri = $"{_configuration.NotificationServiceUri}/api/notification";

            var response = await fullUri.PostJsonAsync(postData);
            response.EnsureSuccessStatusCode();
        }
    }

    public class NotificationMessage
    {
        public NotificationMessage(
            string category, 
            string description, 
            string entityType, 
            string entityId, 
            string portfolioId, 
            string who)
        {
            Category = category;
            Description = description;
            EntityType = entityType;
            EntityId = entityId;
            PortfolioId = portfolioId;
            Who = who;
        }

        public string Category { get; set; }

        public string Description { get; set; }

        public string EntityType { get; set; }

        public string EntityId { get; set; }

        public string PortfolioId { get; set; }

        public string Who { get; set; }
    }
}