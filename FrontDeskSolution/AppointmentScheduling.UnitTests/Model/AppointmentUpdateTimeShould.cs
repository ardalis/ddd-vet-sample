using System;
using AppointmentScheduling.Core.Model.Events;
using AppointmentScheduling.Core.Model.ScheduleAggregate;
using FrontDesk.SharedKernel;
using NUnit.Framework;

namespace AppointmentScheduling.UnitTests.Model
{
    [TestFixture]
    public class AppointmentUpdateTimeShould
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

        [SetUp]
        public void SetUp()
        {
            DomainEvents.ClearCallbacks();

            testAppointment1 = Appointment.Create(testScheduleId,
                testClientId, testPatientId, testRoomId, testStartTime, testEndTime,
                testAppointmentTypeId, testDoctorId, "testAppointment1");

        }

        [Test]
        public void RaiseAppointmentUpdatedEvent()
        {
            // Arrange
            Guid updatedAppointmentId = Guid.Empty;
            DomainEvents.Register<AppointmentUpdatedEvent>(aue => updatedAppointmentId = testAppointment1.Id);

            // Act
            testAppointment1.UpdateTime(newAppointmentTimeRange);

            // Assert
            Assert.AreEqual(testAppointment1.Id, updatedAppointmentId);
        }

        [Test]
        public void NotRaiseAppointmentUpdatedEventWhenTimeRangeDoesNotChange()
        {
            // Arrange
            Guid updatedAppointmentId = Guid.Empty;
            DomainEvents.Register<AppointmentUpdatedEvent>(aue => updatedAppointmentId = testAppointment1.Id);

            // Act
            testAppointment1.UpdateTime(testAppointment1.TimeRange);

            // Assert
            Assert.AreEqual(Guid.Empty, updatedAppointmentId);
        }

        [Test]
        public void UpdateTimeRangeToNewTimeRange()
        {
            // Act
            testAppointment1.UpdateTime(newAppointmentTimeRange);

            // Assert
            Assert.AreEqual(newAppointmentTimeRange, testAppointment1.TimeRange);
        }
    }
}