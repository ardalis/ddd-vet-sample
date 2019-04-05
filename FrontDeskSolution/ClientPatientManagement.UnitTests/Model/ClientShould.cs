using System;
using System.Collections.Generic;
using Common;
using System.Collections;
using NUnit.Framework;
using ClientPatientManagement.Core.Model;

namespace ClientPatientManagement.UnitTests.Model
{
    [TestFixture]
    public class ClientShould
    {
        //[Test]
        //public void HaveAFewNameProperties()
        //{
        //    var client = new Client();
        //    string firstName = "Test First Name";
        //    string lastName = "Test Last Name";
        //    string salutation = "Mr.";
        //    string preferredName = "Billy Joe";

        //    client.FullName = new FullName(firstName, lastName);
        //    client.Salutation = salutation;
        //    client.PreferredName = preferredName;

        //    Assert.AreEqual(firstName, client.FullName.FirstName);
        //    Assert.AreEqual(lastName, client.FullName.LastName);
        //    Assert.AreEqual(salutation, client.Salutation);
        //    Assert.AreEqual(preferredName, client.PreferredName);
        //}

        //[Test]
        //public void ReturnFormattedNameWithAllFieldsAsToString()
        //{
        //    var client = new Client()
        //    {
        //        FullName = new FullName("Steven", "Smith"),
        //        Salutation = "Mr.",
        //        PreferredName = "Steve"
        //    };

        //    var expectedResult = "Mr. Steven Smith (Steve)";

        //    Assert.AreEqual(expectedResult, client.ToString());
        //}

        //[Test]
        //public void ReturnFormattedNameWithNoPreferredNameAsToString()
        //{
        //    var client = new Client()
        //    {
        //        FullName = new FullName("Steven", "Smith"),
        //        Salutation = "Mr."
        //    };

        //    var expectedResult = "Mr. Steven Smith";

        //    Assert.AreEqual(expectedResult, client.ToString());
        //}

        [Test]
        public void HaveAListOfPatients()
        {
            var client = new Client();

            var patients = client.Patients;

            Assert.IsNotNull(patients as IList<Patient>);
        }
    }
}
