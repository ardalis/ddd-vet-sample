using AppointmentScheduling.Core.Model.Events;
using FrontDesk.SharedKernel.Interfaces;
using Microsoft.AspNet.SignalR;

namespace FrontDesk.Web.Hubs
{
    public class AppointmentConfirmedHandler : IHandle<AppointmentConfirmedEvent>
    {
        public void Handle(AppointmentConfirmedEvent args)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ScheduleHub>();
            hubContext.Clients.All.showAlert(args.AppointmentUpdated.Title + " was confirmed");
        }
    }
}