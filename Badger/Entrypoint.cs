using Autofac;
using Badger.Config;

namespace Badger
{
    class Entrypoint
    {
        static void Main(string[] args)
        {
            using (var container = new ContainerFactory().GetContainer())
            {
                var program = container.Resolve<Program>();
                program.Run(args);
            }
        }
    }
}
