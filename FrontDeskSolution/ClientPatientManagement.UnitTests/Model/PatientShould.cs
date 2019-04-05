using System;
using FrontDesk.SharedKernel.Enums;
using NUnit.Framework;
using ClientPatientManagement.Core.Model;

namespace FrontDesk.UnitTests.Model
{
    [TestFixture]
    public class PatientShould
    {
        [Test]
        public void HaveAFewProperties()
        {
            var testName = "Darwin";
            var testGender = Gender.Male;
            var patient = new Patient(new Client())
            {
                Name = testName,
                Gender = testGender
            };

            Assert.AreEqual(testName, patient.Name);
            Assert.AreEqual(testGender, patient.Gender);
        }

        [Test]
        public void HaveAnOwner()
        {
            var client = new Client();
            var patient = new Patient(client);

            Assert.AreEqual(client, patient.Owner);

        }

        [Test]
        public void HavePreferredDoctorDefaultToClientPreferredDoctor()
        {
            var client = new Client();
            var doctor = new Doctor();
            client.PreferredDoctorId = doctor.Id;

            var patient = new Patient(client);

            Assert.AreEqual(client.PreferredDoctorId, patient.PreferredDoctorId);

        }
    }
}
