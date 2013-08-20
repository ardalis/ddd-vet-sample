using System;
using FrontDesk.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrontDesk.UnitTests.Model
{
    [TestClass]
    public class ClientShould
    {
        [TestMethod]
        public void HaveAFewNameProperties()
        {
            var client = new Client();
            string firstName = "Test First Name";
            string lastName = "Test Last Name";
            string salutation = "Mr.";
            string preferredName = "Billy Joe";

            client.FirstName = firstName;
            client.LastName = lastName;
            client.Salutation = salutation;
            client.PreferredName = preferredName;

            Assert.AreEqual(firstName, client.FirstName);
            Assert.AreEqual(lastName, client.LastName);
            Assert.AreEqual(salutation, client.Salutation);
            Assert.AreEqual(preferredName, client.PreferredName);
        }
    }
}
