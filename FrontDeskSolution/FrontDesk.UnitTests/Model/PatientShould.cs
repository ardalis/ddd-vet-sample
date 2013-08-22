using System;
using FrontDesk.Core.Enums;
using FrontDesk.Core.Model.ClientAggregate;
using FrontDesk.Core.Model.PatientAggregate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrontDesk.UnitTests.Model
{
    [TestClass]
    public class PatientShould
    {
        [TestMethod]
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

        [TestMethod]
        public void HaveAnOwner()
        {
            var client = new Client();
            var patient = new Patient(client);

            Assert.AreEqual(client, patient.Owner);

        }
    }
}
