using System;
using AppointmentScheduling.Core.Model.ScheduleAggregate;
using FrontDesk.SharedKernel.Interfaces;

namespace AppointmentScheduling.Core.Model.Events
{
    public class AppointmentScheduledEvent : IDomainEvent
    {
        public AppointmentScheduledEvent(Appointment appointment) : this()
        {
            AppointmentScheduled = appointment;
        }

        public AppointmentScheduledEvent()
        {
            this.Id = Guid.NewGuid();
            DateTimeEventOccurred = DateTime.Now;
        }

        public Guid Id { get; private set; }

        public DateTime DateTimeEventOccurred { get; private set; }

        public Appointment AppointmentScheduled { get; private set; }
    }
}