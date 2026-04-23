using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Maven.Web.Hubs
{
    public class SubastaHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var usuarioId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrWhiteSpace(usuarioId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"usuario-{usuarioId}");
            }

            await base.OnConnectedAsync();  
        }
        public async Task UnirseASubasta(int subastaId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"subasta-{subastaId}");
        }

        public async Task SalirDeSubasta(int subastaId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"subasta-{subastaId}");
        }
    }
}