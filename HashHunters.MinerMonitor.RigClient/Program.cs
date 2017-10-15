using Autofac;
using HashHunters.MinerMonitor.Common;
using HashHunters.MinerMonitor.Common.Interfaces;

namespace HashHunters.MinerMonitor.RigClient
{
    public class Program
    {
        private static IApp App;

        static void Main(string[] args)
        {
            var container = AutofacConfig.Configure(new ClientModule());

            using (var scope = container.BeginLifetimeScope())
            {
                App = scope.Resolve<IApp>();
                App.Run();
            }
        }
    }

    public class ClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<ClientApp>().As<IApp>();
        }
    }
}
