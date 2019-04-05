using System;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class EntityWithGuidKeyEqualsShould
    {
        public class TestGuidEntity : Entity<Guid>
        {
            public TestGuidEntity(Guid id) : base(id)
            {
            }
        }

        [Test]
        public void ReturnTrueGivenTwoEntitiesWithSameGuidId()
        {
            Guid key = Guid.NewGuid();
            var entity1 = new TestGuidEntity(key);
            var entity2 = new TestGuidEntity(key);

            Assert.IsTrue(entity1.Equals(entity2));
        }

        [Test]
        public void ReturnFalseGivenTwoEntitiesWithDifferentGuidId()
        {
            var entity1 = new TestGuidEntity(Guid.NewGuid());
            var entity2 = new TestGuidEntity(Guid.NewGuid());

            Assert.IsFalse(entity1.Equals(entity2));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowGivenEmptyGuid()
        {
            var entity1 = new TestGuidEntity(Guid.Empty);

            Assert.Fail("Should have thrown an exception.");
        }
    }
}