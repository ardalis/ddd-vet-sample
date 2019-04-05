using System;
using NUnit.Framework;
using AppointmentScheduling.Core.Model.ScheduleAggregate;

namespace AppointmentScheduling.UnitTests.Model
{
    [TestFixture]
    public class AppointmentCreateShould
    {
        private int invalidId = 0;
        private int testPatientId = 123;
        private int testClientId = 456;
        private int testRoomId = 567;
        private int testAppointmentTypeId = 1;
        private int testDoctorId = 2;
        private Guid testScheduleId = Guid.Empty;
        private DateTime testStartTime = new DateTime(2014, 7, 1, 9, 0, 0);
        private DateTime testEndTime = new DateTime(2014, 7, 1, 9, 30, 0);
        private string testTitle = "(WE) Darwin - Steve Smith";

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            MatchType = MessageMatch.Contains, ExpectedMessage = "clientId")]
        public void ThrowExceptionGivenInvalidClientId()
        {
            var appointment = Appointment.Create(testScheduleId, invalidId, testPatientId, testRoomId, testStartTime, testEndTime,
                testAppointmentTypeId, null, testTitle);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            MatchType = MessageMatch.Contains, ExpectedMessage = "patientId")]
        public void ThrowExceptionGivenInvalidPatientId()
        {
            var appointment = Appointment.Create(testScheduleId, testClientId, invalidId, testRoomId, testStartTime, testEndTime,
                testAppointmentTypeId, null, testTitle);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            MatchType = MessageMatch.Contains, ExpectedMessage = "roomId")]
        public void ThrowExceptionGivenInvalidRoomId()
        {
            var appointment = Appointment.Create(testScheduleId, testClientId, testPatientId, invalidId, testStartTime, testEndTime,
                testAppointmentTypeId, null, testTitle);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            MatchType = MessageMatch.Contains, ExpectedMessage = "appointmentTypeId")]
        public void ThrowExceptionGivenInvalidAppointmentTypeId()
        {
            var appointment = Appointment.Create(testScheduleId, testClientId, testPatientId, testRoomId, testStartTime, testEndTime,
                invalidId, null, testTitle);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            MatchType = MessageMatch.Contains, ExpectedMessage = "title")]
        public void ThrowExceptionGivenInvalidTitle()
        {
            var appointment = Appointment.Create(testScheduleId, testClientId, testPatientId, testRoomId, testStartTime, testEndTime,
                testAppointmentTypeId, null, String.Empty);
        }

        [Test]
        public void RelateToAClientAndPatient()
        {
            var appointment = Appointment.Create(testScheduleId, testClientId, testPatientId, testRoomId, testStartTime, testEndTime, testAppointmentTypeId, testDoctorId, testTitle);

            Assert.AreEqual(testPatientId, appointment.PatientId);
            Assert.AreEqual(testClientId, appointment.ClientId);
            Assert.AreEqual(testRoomId, appointment.RoomId);
            Assert.AreEqual(testAppointmentTypeId, appointment.AppointmentTypeId);
            Assert.AreEqual(testTitle, appointment.Title);
            Assert.AreEqual(testDoctorId, appointment.DoctorId);
        }

        [Test]
        public void SetDoctorIdToDefaultValueGivenNull()
        {
            var appointment = Appointment.Create(testScheduleId, testClientId, testPatientId, testRoomId, testStartTime, testEndTime, testAppointmentTypeId, null, testTitle);

            Assert.AreEqual(1, appointment.DoctorId);
        }

    }
}
