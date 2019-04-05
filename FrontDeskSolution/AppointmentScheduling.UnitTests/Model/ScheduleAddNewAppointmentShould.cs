using System;
using System.Collections.Generic;
using System.Linq;
using AppointmentScheduling.Core.Model.ScheduleAggregate;
using FrontDesk.SharedKernel;
using NUnit.Framework;
using AppointmentScheduling.Core.Model.Events;

namespace AppointmentScheduling.UnitTests.Model
{
    [TestFixture]
    public class ScheduleAddNewAppointmentShould
    {
        private Guid testScheduleId = Guid.NewGuid();
        private int testClinicId = 1;
        private DateTimeRange testDateTimeRange;
        private Appointment testAppointment1;
        private Appointment testAppointment2;
        private int testPatientId = 123;
        private int testClientId = 456;
        private int testRoomId = 567;
        private int testAppointmentTypeId = 1;
        private int testDoctorId = 2;
        private DateTime testStartTime = new DateTime(2014, 6, 9, 9, 0, 0);
        private DateTime testEndTime = new DateTime(2014, 6, 9, 9, 30, 0);

        [SetUp]
        public void SetUp()
        {
            DomainEvents.ClearCallbacks();

            testDateTimeRange = new DateTimeRange(new DateTime(2014, 6, 9), new DateTime(2014, 6, 16));

            testAppointment1 = Appointment.Create(testScheduleId,
                testClientId, testPatientId, testRoomId, testStartTime, testEndTime,
                testAppointmentTypeId, testDoctorId, "testAppointment1");

            testAppointment2 = Appointment.Create(testScheduleId,
                testClientId, testPatientId, testRoomId, testStartTime, testEndTime,
                testAppointmentTypeId, testDoctorId, "testAppointment2");

        }

        [Test]
        public void AddAppointmentToScheduleListOfAppointments()
        {
            var schedule = GetTestSchedule();
            Assert.AreEqual(0, schedule.Appointments.Count());
            
            schedule.AddNewAppointment(testAppointment1);
            Assert.AreEqual(testAppointment1, schedule.Appointments.First());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), MatchType = MessageMatch.Contains, ExpectedMessage = "duplicate")]
        public void ThrowExceptionIfSameExactAppointmentIsAddedTwice()
        {
            var schedule = GetTestSchedule();

            schedule.AddNewAppointment(testAppointment1);
            schedule.AddNewAppointment(testAppointment1);

            Assert.Fail("Should throw exception before reaching this point.");
        }

        [Test]
        public void RaiseAppointmentScheduledEvent()
        {
            // Arrange
            Guid scheduledAppointmentId = Guid.Empty;
            DomainEvents.Register<AppointmentScheduledEvent>(ase => scheduledAppointmentId = testAppointment1.Id);

            // Act
            var schedule = GetTestSchedule();
            schedule.AddNewAppointment(testAppointment1);

            // Assert
            Assert.AreEqual(testAppointment1.Id, scheduledAppointmentId);
        }

        [Test]
        public void MarkAppointmentsAsConflictedWhenSamePatientIsInTwoRoomsAtSameTime()
        {
            var schedule = GetTestSchedule();
            schedule.AddNewAppointment(testAppointment1);
            Assert.IsFalse(testAppointment1.IsPotentiallyConflicting);

            // same patient in a different room at same time
            int differentRoomId = 123;
            var conflictingAppointment = Appointment.Create(testScheduleId, testAppointment1.ClientId, testAppointment1.PatientId,
                differentRoomId, testStartTime, testEndTime, testAppointmentTypeId, testDoctorId, "CONFLICTING APPOINTMENT");

            schedule.AddNewAppointment(conflictingAppointment);

            Assert.IsTrue(testAppointment1.IsPotentiallyConflicting, "original appointment should be conflicting");
            Assert.IsTrue(conflictingAppointment.IsPotentiallyConflicting, "new appointment should be conflicting");
        }

        private Schedule GetTestSchedule()
        {
            return new Schedule(testScheduleId, testDateTimeRange, testClinicId, new List<Appointment>());
        }
    }
}