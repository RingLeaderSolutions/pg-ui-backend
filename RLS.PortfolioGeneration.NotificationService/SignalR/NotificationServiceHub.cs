using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace RLS.PortfolioGeneration.NotificationService.SignalR
{
    [Authorize]
    public class NotificationServiceHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Log.Information($"User [{GetUsernameClaim(Context.User)} - {Context.UserIdentifier}] connected. ConnectionId=[{Context.ConnectionId}]");

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            
            Log.Information($"User [{GetUsernameClaim(Context.User)} - {Context.UserIdentifier}] disconnected. ConnectionId=[{Context.ConnectionId}]");
            
            return base.OnDisconnectedAsync(exception);
        }

        private string GetUsernameClaim(ClaimsPrincipal user)
        {
            var username = user?.FindFirst(TPIFlowClaimTypes.Username)?.Value;
            return string.IsNullOrWhiteSpace(username) ? "Unknown" : username;
        }
    }
}