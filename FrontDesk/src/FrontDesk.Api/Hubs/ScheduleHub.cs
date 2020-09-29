using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FrontDesk.Api.Hubs
{
    public class ScheduleHub : Hub
    {
        public async Task UpdateScheduleAsync(string s)
        {
            await Clients.All.SendAsync(s);
        }
    }
}