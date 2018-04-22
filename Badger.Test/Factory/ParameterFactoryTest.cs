using Badger.Factory;
using System;
using Badger.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Badger.Test.Factory
{
    public class ParameterFactoryTest
    {
        IParameterFactory GetFactory() => new ParameterFactory();

        [Test]
        public void ParameterFactoryTest_Ctor()
        {
            GetFactory();
        }

        [Test]
        public void ParameterFactoryTest_GetParameter()
        {
            /* arrange */
            var factory = GetFactory();

            /* act */
            var result = factory.GetParameter("-o test.svg -l build -r passing".Split(" "));

            /* assert */
            result.Should().NotBeNull();
            result.Label.Should().Be("build");
            result.Result.Should().Be("passing");
            result.Action.Should().Be(ActionType.CreateImage);
        }

        [Test]
        public void ParameterFactoryTest_GetParameter_HelpText()
        {
            /* arrange */
            var factory = GetFactory();

            /* act */
            var result = factory.GetParameter(new string[0]);

            /* assert */
            result.Should().NotBeNull();
            result.Action.Should().Be(ActionType.ShowHelp);
            result.HelpText.Should().NotBeNullOrWhiteSpace();
            Console.WriteLine(result.HelpText);
        }
    }
}
