using System;
using FrontDesk.Core.Enums;
using FrontDesk.Core.Model.ClientAggregate;
using FrontDesk.Core.Model.PatientAggregate;
using NUnit.Framework;

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
    }
}
