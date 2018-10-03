using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RLS.PortfolioGeneration.NotificationService.Dtos;
using RLS.PortfolioGeneration.NotificationService.Repositories;
using RLS.PortfolioGeneration.NotificationService.SignalR;
using Serilog;

namespace RLS.PortfolioGeneration.NotificationService.Controllers
{
    [Route("api/[controller]")]
    public sealed class NotificationController : Controller
    {
        private readonly IHubContext<NotificationServiceHub> _hub;
        private readonly InMemoryNotificationRepository _repository;
        
        public NotificationController(
            IHubContext<NotificationServiceHub> hub, 
            InMemoryNotificationRepository repository)
        {
            _hub = hub;
            _repository = repository;
        }

        /// <summary>
        /// Retrieve historic notifications from the NotificationService.
        /// </summary>
        /// <returns>The last 50 notifications that were sent.</returns>
        [HttpGet]
        public List<NotificationMessage> Get()
        {
            return _repository.GetAll().ToList();
        }

        /// <summary>
        /// Send a notification to all connected users.
        /// </summary>
        /// <param name="message">The message to send</param>
        /// <returns>200 OK if the message was sent, 400 Bad Request with message if the notification payload was not successfully validated.</returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NotificationMessage message)
        {
            var validationResult = ValidateMessage(message);
            if (!validationResult.Success)
            {
                Log.Logger.Warning($"Responding to POST /notification with 400: Received invalid payload: {validationResult.Message}");
                return BadRequest($"Invalid payload: {validationResult.Message}");
            }

            _repository.Add(message);
            await _hub.Clients.All.SendAsync("Notify", message);
            return Ok();
        }

        private ValidationResult ValidateMessage(NotificationMessage message)
        {
            if (message != null)
            {
                // If the message is not null, handoff to property validation
                return ValidateNotificationProperties(message);
            }

            return new ValidationResult(false, "Failed to map body to NotificationMessage.");
        }

        private ValidationResult ValidateNotificationProperties(NotificationMessage message)
        {
            var entityTypes = Enum.GetNames(typeof(EntityType))
                .Select(et => et.ToLowerInvariant());
            var validEntityTypes = string.Join('|', entityTypes);

            var validationResults = new Dictionary<string, bool>
            {
                { "Property [category] must not be null, empty or whitespace", !string.IsNullOrWhiteSpace(message.Category) },
                { $"Property [entityType] must successfully map to one of ({validEntityTypes})", message.EntityType.HasValue },
                { "Properties [portfolioId] or [entityId] cannot both be null or empty",  !string.IsNullOrWhiteSpace(message.PortfolioId) || !string.IsNullOrWhiteSpace(message.EntityId) }
            };

            var failures = validationResults
                .Where(kvp => !kvp.Value)
                .Select(kvp => kvp.Key)
                .ToList();

            var failureCount = failures.Count;
            if (failureCount == 0)
            {
                return ValidationResult.Successful;
            }

            var s = failureCount > 1 ? "s" : string.Empty;
            var errorMessage = $"Property validation failed with [{failureCount}] error{s}:{Environment.NewLine}{string.Join(Environment.NewLine, failures)}";

            return ValidationResult.Failed(errorMessage);
        }
        
        private sealed class ValidationResult
        {
            public static readonly ValidationResult Successful = new ValidationResult(true, string.Empty);
            public static ValidationResult Failed(string message)
            {
                return new ValidationResult(false, message);
            }

            public ValidationResult(bool success, string message)
            {
                this.Success = success;
                this.Message = message;
            }

            public bool Success { get; }
            public string Message { get; }
        }
    }
}
