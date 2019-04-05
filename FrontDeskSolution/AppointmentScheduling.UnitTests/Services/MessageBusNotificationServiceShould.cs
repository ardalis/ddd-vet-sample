using System;
using FrontDesk.Core.Interfaces;
using FrontDesk.Core.Services;
using NUnit.Framework;
using Telerik.JustMock;
using FrontDesk.Core.Model.Events;

namespace FrontDesk.UnitTests.Services
{
    [TestFixture]
    public class MessageBusNotificationServiceShould
    {
        //[Test]
        //public void PublishEventToMessageBus()
        //{
        //    // Arrange
        //    var client = new Client();
        //    var patient = new Patient(client);
        //    var patientRegistrationService = Mock.Create<PatientRegistrationService>();
        //    var messageBus = Mock.Create<IMessageBus<NewPatientCreatedEvent>>(Behavior.Strict);

        //    Mock.Arrange(() => messageBus.Publish(Arg.IsAny<NewPatientCreatedEvent>())).OccursOnce();

        //    var myService = new MessageBusNotificationService(patientRegistrationService, messageBus);
        //    myService.Setup();

        //    // Act
        //    Mock.Raise(() => patientRegistrationService.NewPatientCreated += (o, e) => { }, new NewPatientCreatedEventArgs(patient));

        //    // Assert
        //    Mock.Assert(messageBus);
        //}

        //[Test]
        //public void RaiseNewPatientCreatedEvent()
        //{
        //    // Arrange
        //    var client = new Client();
        //    var patient = new Patient(client);
        //    var patientRepository = Mock.Create<IPatientRepository>();

        //    Mock.Arrange(() => patientRepository.CreateOrUpdate(patient));
        //    var service = new PatientRegistrationService(patientRepository);
        //    Patient patientReturnedFromEvent = null;
        //    service.NewPatientCreated += (o, e) =>
        //    {
        //        patientReturnedFromEvent = e.Patient;
        //    };

        //    // Act
        //    service.RegisterPatient(patient);

        //    // Assert
        //    Assert.IsNotNull(patientReturnedFromEvent);
        //}
    }
}
