using System;
using FrontDesk.SharedKernel.Interfaces;

namespace AppointmentScheduling.Core.Model.ApplicationEvents
{
    public class AppointmentConfirmedEvent : IApplicationEvent
    {
        public AppointmentConfirmedEvent()
        {
            this.Id = Guid.NewGuid();
            DateTimeEventOccurred = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime DateTimeEventOccurred { get; set; }
        public Guid AppointmentId { get; set; }
        public string EventType
        {
            get
            {
                return "AppointmentConfirmedEvent";
            }
        }
    }
}