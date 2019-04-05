using AppointmentScheduling.Core.Model.Events;
using FrontDesk.SharedKernel.Interfaces;
using Microsoft.AspNet.SignalR;

namespace FrontDesk.Web.Hubs
{
    public class AppointmentScheduledHandler : IHandle<AppointmentScheduledEvent>
    {
        public void Handle(AppointmentScheduledEvent args)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ScheduleHub>();
            hubContext.Clients.All.showAlert(args.AppointmentScheduled.Title + " was JUST SCHEDULED");
        }
    }
}