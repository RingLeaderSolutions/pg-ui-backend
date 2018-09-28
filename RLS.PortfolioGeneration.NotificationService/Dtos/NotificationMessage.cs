using System;
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

    public sealed class RecordedNotificationMessage : NotificationMessage
    {
        public RecordedNotificationMessage(NotificationMessage message)
        {
            Category = message.Category;
            Description = message.Description;
            EntityType = message.EntityType;
            EntityId = message.EntityId;
            PortfolioId = message.PortfolioId;
            Who = message.Who;

            Received = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset Received { get;  }
    }
}