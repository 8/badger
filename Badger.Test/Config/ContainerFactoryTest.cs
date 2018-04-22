using System;
using Autofac;
using Badger.Config;
using Badger.Factory;
using Badger.Service;
using NUnit.Framework;

namespace Badger.Test.Config
{
    [TestFixture]
    public class ContainerFactoryTest
    {
        ContainerFactory GetFactory() => new ContainerFactory();

        [Test]
        public void ContainerFactoryTest_Ctor()
        {
            GetFactory();
        }

        [Test]
        public void ContainerFactoryTest_GetContainer()
        {
            using (var container = GetFactory().GetContainer()) { }
        }

        [Test,
            TestCase(typeof(Program)),
            TestCase(typeof(IParameterFactory)),
            TestCase(typeof(ISvgService))
        ]
        public void ContainerFactoryTest_Resolve(Type t)
        {
            var factory = GetFactory();

            using (var container = factory.GetContainer())
            {
                var instance = container.Resolve(t);
            }
        }
    }
}
