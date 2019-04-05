using FrontDesk.SharedKernel;
using NUnit.Framework;
using System;
using System.Linq;

namespace SharedKernel.UnitTests
{
    [TestFixture]
    class DateTimeRangeShould
    {
        private DateTime _testStartDate = new DateTime(1, 1, 2014, 9, 0, 0);

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowArgumentExceptionIfStartDateEqualsEndDate()
        {
            var result = new DateTimeRange(_testStartDate, _testStartDate);
        }
    }
}
