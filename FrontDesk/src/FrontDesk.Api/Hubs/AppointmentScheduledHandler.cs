using FrontDesk.Core.Events;
using FrontDesk.SharedKernel.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FrontDesk.Api.Hubs
{
    public class AppointmentScheduledHandler : IHandle<AppointmentScheduledEvent>
    {
        private readonly IHubContext<ScheduleHub> _hubContext;

        public AppointmentScheduledHandler(IHubContext<ScheduleHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleAsync(AppointmentScheduledEvent args)
        {
            await _hubContext.Clients.All.SendAsync(args.AppointmentScheduled.Title + " was JUST SCHEDULED");
        }
    }
}