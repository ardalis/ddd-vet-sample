using FrontDesk.Core.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace FrontDesk.Api.Hubs
{
    public class AppointmentScheduledHandler : INotificationHandler<AppointmentScheduledEvent>
    {
        private readonly IHubContext<ScheduleHub> _hubContext;

        public AppointmentScheduledHandler(IHubContext<ScheduleHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(AppointmentScheduledEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", notification.AppointmentScheduled.Title + " was JUST SCHEDULED");
        }
    }
}