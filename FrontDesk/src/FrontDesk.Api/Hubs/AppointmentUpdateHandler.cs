using FrontDesk.Core.Events;
using FrontDesk.SharedKernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace FrontDesk.Api.Hubs
{
    public class AppointmentUpdateHandler : INotificationHandler<AppointmentUpdatedEvent>
    {
        private readonly IHubContext<ScheduleHub> _hubContext;

        public AppointmentUpdateHandler(IHubContext<ScheduleHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(AppointmentUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", notification.AppointmentUpdated.Title + " was updated");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });

    }
}