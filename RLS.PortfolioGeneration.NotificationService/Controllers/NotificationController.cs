using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RLS.PortfolioGeneration.NotificationService.Dtos;
using RLS.PortfolioGeneration.NotificationService.Repositories;
using RLS.PortfolioGeneration.NotificationService.SignalR;
using RLS.PortfolioGeneration.Persistence.Model;

namespace RLS.PortfolioGeneration.NotificationService.Controllers
{
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly IHubContext<NotificationServiceHub> _hub;
        private readonly ModelDbContext _dbContext;
        private readonly NotificationMessageRepository _repository;


        public NotificationController(IHubContext<NotificationServiceHub> hub, ModelDbContext dbContext, NotificationMessageRepository repository)
        {
            _hub = hub;
            _dbContext = dbContext;
            _repository = repository;
        }

        [HttpGet]
        public List<NotificationMessage> Get()
        {
            return _repository.GetAll().ToList();
        }

        [HttpPost]
        public async Task Post([FromBody]NotificationMessage message)
        {
            if (await NotifyClients(message))
            {
                _repository.Add(message);
                Ok();
            }

            NotFound();
        }

        private async Task<bool> NotifyClients(NotificationMessage message)
        {
            if (message.EntityType == null)
            {
                return false;
            }

            await _hub.Clients.All.InvokeAsync("Notify", message);
            return true;
        }
    }
}
