using System;
using System.Collections.Generic;
using NUnit.Framework;
using AppointmentScheduling.Core.Model.ScheduleAggregate;
using FrontDesk.SharedKernel;

namespace AppointmentScheduling.UnitTests.Model
{
    [TestFixture]
    public class ScheduleShould
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
        public void BeAnEntity()
        {
            var appointment = new Appointment(Guid.NewGuid());

            Assert.IsInstanceOf<Entity<Guid>>(appointment);
        }

        [Test]
        public void MarkConflictingAppointmentsUponCreation()
        {
            var schedule = new Schedule(Guid.NewGuid(), testDateTimeRange, testClinicId, new List<Appointment>() { testAppointment1, testAppointment2 });

            Assert.IsTrue(testAppointment1.IsPotentiallyConflicting);
            Assert.IsTrue(testAppointment2.IsPotentiallyConflicting);
        }

        [Test]
        public void UnmarkConflictingAppointmentsUponTheirUpdate()
        {
            // Arrange
            var schedule = new Schedule(Guid.NewGuid(), testDateTimeRange, testClinicId, new List<Appointment>() { testAppointment1, testAppointment2 });

            // Act - should raise an event Schedule should handle and unmark conflict
            testAppointment1.UpdateTime(new DateTimeRange(testAppointment1.TimeRange.Start.AddDays(1), testAppointment1.TimeRange.End.AddDays(1)));

            Assert.IsFalse(testAppointment1.IsPotentiallyConflicting);
            Assert.IsFalse(testAppointment2.IsPotentiallyConflicting);
        }
    }
}
