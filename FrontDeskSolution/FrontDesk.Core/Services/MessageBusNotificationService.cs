using System;
using FrontDesk.Core.Interfaces;
using FrontDesk.Core.Model.Events;

namespace FrontDesk.Core.Services
{
    /// <summary>
    /// We need to wire this up in the FrontDesk startup
    /// It should be responsible for pushing events to the service bus for all domain events and all domain services that have these events
    /// </summary>
    public class MessageBusNotificationService
    {
        private readonly PatientRegistrationService _patientRegistrationService;

        private readonly IMessageBus<NewPatientCreatedEvent> _messageBus;

        public MessageBusNotificationService(PatientRegistrationService patientRegistrationService,
            IMessageBus<NewPatientCreatedEvent> messageBus)
        {
            this._messageBus = messageBus;
            this._patientRegistrationService = patientRegistrationService;
        }

        public void Setup()
        {
            _patientRegistrationService.NewPatientCreated += PatientRegistrationServiceNewPatientCreated;
        }

        void PatientRegistrationServiceNewPatientCreated(object sender, NewPatientCreatedEventArgs e)
        {
            _messageBus.Publish(new NewPatientCreatedEvent()
            {
                ClientEmailAddress="",
                ClientLastName="",
                PatientName = e.Patient.Name,
                DateRegistered = DateTime.Now
            });
        }
    }
}