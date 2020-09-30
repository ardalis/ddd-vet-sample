using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FrontDesk.Api.Hubs
{
    public class ScheduleHub : Hub
    {
        public async Task UpdateScheduleAsync(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}