using FrontDesk.Core.Events;
using FrontDesk.SharedKernel.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FrontDesk.Api.Hubs
{
    public class AppointmentConfirmedHandler : IHandle<AppointmentConfirmedEvent>
    {
        private readonly IHubContext<ScheduleHub> _hubContext;

        public AppointmentConfirmedHandler(IHubContext<ScheduleHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleAsync(AppointmentConfirmedEvent args)
        {
            await _hubContext.Clients.All.SendAsync(args.AppointmentUpdated.Title + " was confirmed");
        }
    }
}