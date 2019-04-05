using System.Data.Entity;
using System.Linq;
using AppointmentScheduling.Core.Model.ScheduleAggregate;
using NUnit.Framework;
using AppointmentScheduling.Data;

namespace AppointmentScheduling.IntegrationTests.Data
{
    [TestFixture]
    public class SchedulingContextShould
    {
        public SchedulingContextShould()
        {
            Database.SetInitializer<SchedulingContext>(null);
        }

        [Test]
        public void GetClientReferenceList()
        {
            var db = new SchedulingContext();
            Assert.IsNotEmpty(db.Clients.ToList());
        }

        [Test]
        public void ReturnSchedulingClientType()
        {
            var db = new SchedulingContext();
            Assert.IsInstanceOf<AppointmentScheduling.Core.Model.ScheduleAggregate.Client>(db.Clients.FirstOrDefault());
        }

        [Test]
        public void ReturnClientsWithPatients()
        {
            using (var db = new SchedulingContext())
            {
                Assert.IsNotEmpty(db.Clients.Include(c => c.Patients).ToList());
            }
        }
    }
}
