using System;
using AppointmentScheduling.Core.Model.Events;
using AppointmentScheduling.Core.Model.ScheduleAggregate;
using FrontDesk.SharedKernel;
using NUnit.Framework;

namespace AppointmentScheduling.UnitTests.Model
{
    [TestFixture]
    public class AppointmentConfirmShould
    {
        private Guid testScheduleId = Guid.NewGuid();
        private Appointment testAppointment1;
        private int testPatientId = 123;
        private int testClientId = 456;
        private int testRoomId = 567;
        private int testAppointmentTypeId = 1;
        private int testDoctorId = 2;
        private DateTime testStartTime = new DateTime(2014, 6, 9, 9, 0, 0);
        private DateTime testEndTime = new DateTime(2014, 6, 9, 9, 30, 0);
        private DateTimeRange newAppointmentTimeRange = new DateTimeRange(new DateTime(2014, 6, 9, 10, 0, 0), TimeSpan.FromHours(1));
        private DateTime testConfirmationTime = DateTime.Now;

        [SetUp]
        public void SetUp()
        {
            DomainEvents.ClearCallbacks();

            testAppointment1 = Appointment.Create(testScheduleId,
                testClientId, testPatientId, testRoomId, testStartTime, testEndTime,
                testAppointmentTypeId, testDoctorId, "testAppointment1");

        }

        [Test]
        public void RaiseAppointmentConfirmedEvent()
        {
            // Arrange
            Guid confirmedAppointmentId = Guid.Empty;
            DomainEvents.Register<AppointmentConfirmedEvent>(aue => confirmedAppointmentId = testAppointment1.Id);

            // Act
            testAppointment1.Confirm(testConfirmationTime);

            // Assert
            Assert.AreEqual(testAppointment1.Id, confirmedAppointmentId);
        }

        [Test]
        public void NotRaiseAppointmentConfirmedEventWhenAppointmentAlreadyConfirmed()
        {
            // Arrange
            testAppointment1.Confirm(testConfirmationTime);
            Guid confirmedAppointmentId = Guid.Empty;
            DomainEvents.Register<AppointmentConfirmedEvent>(aue => confirmedAppointmentId = testAppointment1.Id);

            // Act
            testAppointment1.Confirm(testConfirmationTime);

            // Assert
            Assert.AreEqual(Guid.Empty, confirmedAppointmentId);
        }

        [Test]
        public void UpdateTimeRangeToNewTimeRange()
        {
            // Act
            testAppointment1.Confirm(testConfirmationTime);

            // Assert
            Assert.AreEqual(testConfirmationTime, testAppointment1.DateTimeConfirmed.Value);
        }
    }
}