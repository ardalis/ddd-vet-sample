using System;
using AppointmentScheduling.Core.Model.Events;
using AppointmentScheduling.Core.Model.ScheduleAggregate;
using FrontDesk.SharedKernel.Interfaces;
using Microsoft.AspNet.SignalR;

namespace FrontDesk.Web.Hubs
{
    public class AppointmentUpdateHandler : IHandle<AppointmentUpdatedEvent>
    {
        public void Handle(AppointmentUpdatedEvent args)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ScheduleHub>();
            hubContext.Clients.All.showAlert(args.AppointmentUpdated.Title + " was updated");
        }
    }
    public class AppointmentScheduledHandler : IHandle<AppointmentScheduledEvent>
    {
        public void Handle(AppointmentScheduledEvent args)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ScheduleHub>();
            hubContext.Clients.All.showAlert(args.AppointmentScheduled.Title + " was JUST SCHEDULED");
        }
    }
}