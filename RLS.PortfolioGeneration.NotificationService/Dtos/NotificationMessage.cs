using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RLS.PortfolioGeneration.NotificationService.Dtos
{
    public class NotificationMessage
    {
        public string Category { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntityType? EntityType { get; set; }

        public string EntityId { get; set; }

        public string PortfolioId { get; set; }

        public string Who { get; set; }
    }


    public enum EntityType
    {
        Portfolio,
        PortfolioMeters,
        Tender,
        Account
    }
}