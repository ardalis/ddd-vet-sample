using DomainEventsConsole.Interfaces;
using DomainEventsConsole.Model;
using System;

namespace DomainEventsConsole.Events
{
    public class AppointmentConfirmed : IDomainEvent
    {
        public Appointment Appointment { get; set; }
        public System.DateTime DateOccurred { get; private set; }

        public AppointmentConfirmed(Appointment appointment, DateTime dateConfirmed)
        {
            this.Appointment = appointment;
            this.DateOccurred = dateConfirmed;
        }
        public AppointmentConfirmed(Appointment appointment) : this(appointment, DateTime.Now)
        {
        }
    }
}