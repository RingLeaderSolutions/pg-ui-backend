using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace RLS.PortfolioGeneration.NotificationService.SignalR
{
    public sealed class NameIdentifierUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}