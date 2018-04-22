using Autofac;
using Badger.Factory;
using Badger.Service;
using System.Reflection;

namespace Badger.Config
{
    public class ContainerFactory
    {
        public ILifetimeScope GetContainer()
        {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();

            /* register the program */
            builder.RegisterType<Program>().AsSelf().SingleInstance();

            /* register all factories */
            builder.RegisterAssemblyTypes(assembly)
                .InNamespaceOf<IParameterFactory>()
                .AsImplementedInterfaces();

            /* register all services as singletons */
            builder.RegisterAssemblyTypes(assembly)
                .InNamespaceOf<ISvgService>()
                .AsImplementedInterfaces()
                .SingleInstance();

            return builder.Build();
        }
    }
}
