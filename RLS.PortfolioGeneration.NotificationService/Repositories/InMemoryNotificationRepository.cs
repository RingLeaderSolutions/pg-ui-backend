using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using RLS.PortfolioGeneration.NotificationService.Dtos;

namespace RLS.PortfolioGeneration.NotificationService.Repositories
{
    public sealed class InMemoryNotificationRepository
    {
        private const int MessageLimit = 50;
        private readonly ConcurrentQueue<RecordedNotificationMessage> _recordedNotifications 
            = new ConcurrentQueue<RecordedNotificationMessage>();
        
        public void Add(NotificationMessage message)
        {
            var recordedMessage = new RecordedNotificationMessage(message);
            _recordedNotifications.Enqueue(recordedMessage);

            while (_recordedNotifications.Count > MessageLimit)
            {
                _recordedNotifications.TryDequeue(out recordedMessage);
            }
        }

        public IEnumerable<NotificationMessage> GetAll()
        {
            return _recordedNotifications.ToList();
        }
    }
}