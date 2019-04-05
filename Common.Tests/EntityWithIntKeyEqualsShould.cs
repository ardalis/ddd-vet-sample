using System;
using System.Linq;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class EntityWithIntKeyEqualsShould
    {
        public class TestIntEntity : Entity<int>
        {
            public TestIntEntity(int id)
                : base(id)
            {
            }
        }

        [Test]
        public void ReturnTrueGivenTwoEntitiesWithSameIntegerId()
        {
            var entity1 = new TestIntEntity(1);
            var entity2 = new TestIntEntity(1);

            Assert.IsTrue(entity1.Equals(entity2));
        }

        [Test]
        public void ReturnFalseGivenTwoEntitiesWithDifferentIntegerId()
        {
            var entity1 = new TestIntEntity(1);
            var entity2 = new TestIntEntity(2);

            Assert.IsFalse(entity1.Equals(entity2));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowGivenZeroId()
        {
            var entity1 = new TestIntEntity(0);

            Assert.Fail("Should have thrown an exception.");
        }

    }
}