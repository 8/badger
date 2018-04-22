using Badger.Factory;
using Badger.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Badger.Test.Factory
{
    [TestFixture]
    public class BadgeFactoryTest
    {
        private IBadgeFactory GetFactory()
        {
            return new BadgeFactory();
        }

        [Test]
        public void BadgeFactoryTest_Ctor()
        {
            GetFactory();
        }

        [Test]
        public void BadgeFactoryTest_Get()
        {
            /* arrange */
            var factory = GetFactory();

            /* act */
            var result = factory.GetBadge(new ParameterModel());

            /* assert */
            result.Should().NotBeNull();
            result.LabelBackgroundColor.Alpha.Should().Be(255);
        }
    }
}
