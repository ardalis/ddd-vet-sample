using FrontDesk.Core.Events;
using FrontDesk.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace FrontDesk.Api.Hubs
{
    public class AppointmentUpdateHandler : IHandle<AppointmentUpdatedEvent>
    {
        public async Task HandleAsync(AppointmentUpdatedEvent args)
        {
            var host = CreateHostBuilder(null).Build();
            var hubContext = (IHubContext<ScheduleHub>)host.Services.GetService(typeof(IHubContext<ScheduleHub>));

            await hubContext.Clients.All.SendAsync(args.AppointmentUpdated.Title + " was updated");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}