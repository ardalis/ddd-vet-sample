using BlazorShared.Models.Appointment;
using FrontDesk.SharedKernel;
using System;

namespace FrontDesk.Core.Events.ApplicationEvents
{
    public class AppointmentScheduledEvent : BaseDomainEvent
    {
        public AppointmentScheduledEvent(AppointmentDto appointment) : this()
        {
            AppointmentScheduled = appointment;
        }

        public AppointmentScheduledEvent()
        {
            DateTimeEventOccurred = DateTime.Now;
        }

        public DateTime DateTimeEventOccurred { get; private set; }
        public AppointmentDto AppointmentScheduled { get; private set; }
        public string EventType
        {
            get
            {
                return "AppointmentScheduledEvent";
            }
        }
    }
}