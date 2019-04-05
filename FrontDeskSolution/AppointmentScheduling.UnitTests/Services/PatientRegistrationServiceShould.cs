using System;
using FrontDesk.Core.Interfaces;
using FrontDesk.Core.Model.CrudClasses;
using FrontDesk.Core.Services;
using NUnit.Framework;
using Telerik.JustMock;

namespace FrontDesk.UnitTests.Services
{
    [TestFixture]
    public class PatientRegistrationServiceShould
    {
        [Test]
        public void PersistPatient()
        {
            // Arrange
            var client = new Client();
            var patient = new Patient(client);
            var patientRepository = Mock.Create<IPatientRepository>(Behavior.Strict);

            Mock.Arrange(() => patientRepository.CreateOrUpdate(patient)).OccursOnce();
            var service = new PatientRegistrationService(patientRepository);

            // Act
            service.RegisterPatient(patient);

            // Assert
            Mock.Assert(patientRepository);
        }

        [Test]
        public void RaiseNewPatientCreatedEvent()
        {
            // Arrange
            var client = new Client();
            var patient = new Patient(client);
            var patientRepository = Mock.Create<IPatientRepository>();

            Mock.Arrange(() => patientRepository.CreateOrUpdate(patient));
            var service = new PatientRegistrationService(patientRepository);
            Patient patientReturnedFromEvent = null;
            service.NewPatientCreated += (o, e) =>
            {
                patientReturnedFromEvent = e.Patient;
            };

            // Act
            service.RegisterPatient(patient);

            // Assert
            Assert.IsNotNull(patientReturnedFromEvent);
        }
    }
}
