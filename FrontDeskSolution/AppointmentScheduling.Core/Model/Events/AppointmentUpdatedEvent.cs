using System;
using FrontDesk.SharedKernel.Interfaces;
using AppointmentScheduling.Core.Model.ScheduleAggregate;

namespace AppointmentScheduling.Core.Model.Events
{
    public class AppointmentUpdatedEvent : IDomainEvent
    {
        public AppointmentUpdatedEvent(Appointment appointment)
            : this()
        {
            AppointmentUpdated = appointment;
        }
        public AppointmentUpdatedEvent()
        {
            this.Id = Guid.NewGuid();
            DateTimeEventOccurred = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime DateTimeEventOccurred { get; private set; }
        public Appointment AppointmentUpdated { get; private set; }
    }
}