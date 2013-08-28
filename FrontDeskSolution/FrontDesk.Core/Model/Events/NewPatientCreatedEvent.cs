using System;
using FrontDesk.Core.Interfaces;

namespace FrontDesk.Core.Model.Events
{
    public class NewPatientCreatedEvent : IEvent
    {
        public NewPatientCreatedEvent()
        {
            this.Id = Guid.NewGuid();
        }
        public string PatientName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientEmailAddress { get; set; }
        public DateTime DateRegistered { get; set; }
        public Guid Id { get; private set; }
    }
}