using System;
using NUnit.Framework;
using AppointmentScheduling.Core.Model.ScheduleAggregate;
using FrontDesk.SharedKernel;

namespace AppointmentScheduling.UnitTests.Model
{
    [TestFixture]
    public class AppointmentShould
    {
        [Test]
        public void BeAnEntity()
        {
            var appointment = new Appointment(Guid.NewGuid());

            Assert.IsInstanceOf<Entity<Guid>>(appointment);
        }

        [Test] //move to sharedkerneltests when they are created
        public void DateTimeRangeShouldReturnCorrectDuration()
        {
            var thirtyMinutes = new TimeSpan(0, 0, 30, 0);

            var range = new DateTimeRange(DateTime.Now, thirtyMinutes);
            Assert.AreEqual(thirtyMinutes.Minutes, range.DurationInMinutes());
        }
    }
}
