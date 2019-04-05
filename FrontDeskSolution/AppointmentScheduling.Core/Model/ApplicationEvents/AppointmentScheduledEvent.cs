using System;
using FrontDesk.SharedKernel.Interfaces;

namespace AppointmentScheduling.Core.Model.ApplicationEvents
{
    public class AppointmentScheduledEvent : IApplicationEvent
    {
        public AppointmentScheduledEvent(AppointmentDTO appointment) : this()
        {
            AppointmentScheduled = appointment;
        }

        public AppointmentScheduledEvent()
        {
            DateTimeEventOccurred = DateTime.Now;
        }

        public DateTime DateTimeEventOccurred { get; private set; }
        public AppointmentDTO AppointmentScheduled { get; private set; }
        public string EventType
        {
            get
            {
                return "AppointmentScheduledEvent";
            }
        }
    }
}