using DomainEventsConsole.Events;
using DomainEventsConsole.Interfaces;
using System;
using System.Linq;

namespace DomainEventsConsole.Model
{
    public class Appointment : IEntity
    {
        public Guid Id { get; private set; }
        public string EmailAddress { get; private set; }
        public DateTime? ConfirmationReceivedDate { get; private set; }
        
        protected Appointment() : this(Guid.NewGuid())
        {
        }

        public Appointment(Guid id)
        { 
            this.Id = id;
        }

        public static Appointment Create(string emailAddress)
        {
            Console.WriteLine("Appointment::Create()");

            var appointment = new Appointment();
            appointment.EmailAddress = emailAddress;

            DomainEvents.Raise(new AppointmentCreated(appointment));

            return appointment;
        }

        public void Confirm(DateTime dateConfirmed)
        {
            ConfirmationReceivedDate = dateConfirmed;

            DomainEvents.Raise(new AppointmentConfirmed(this));
        }
    }
}
