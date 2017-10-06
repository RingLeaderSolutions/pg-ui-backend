using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RLS.PortfolioGeneration.NotificationService.Dtos
{
    public class NotificationMessage
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Category? Category { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntityType? EntityType { get; set; }

        public string EntityId { get; set; }
    }


    public enum Category
    {
        Onboard,
        Upload,
        Portfolio
    }


    public enum EntityType
    {
        Portfolio
    }
}