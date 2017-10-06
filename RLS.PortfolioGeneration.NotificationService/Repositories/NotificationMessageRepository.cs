using System.Collections.Generic;
using System.Linq;
using RLS.PortfolioGeneration.NotificationService.Dtos;

namespace RLS.PortfolioGeneration.NotificationService.Repositories
{
    public class NotificationMessageRepository
    {

        private readonly List<NotificationMessage> _messages = new List<NotificationMessage>();


        public void Add(NotificationMessage message)
        {
            _messages.Add(message);
        }

        public IEnumerable<NotificationMessage> GetAll()
        {
            return _messages.Select(m => m);
        }
    }
}