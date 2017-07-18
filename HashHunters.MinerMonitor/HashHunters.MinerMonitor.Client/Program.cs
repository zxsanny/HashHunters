using Autofac;
using HashHunters.MinerMonitor.Common;
using HashHunters.MinerMonitor.Common.Interfaces;

namespace HashHunters.MinerMonitor.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            var container = AutofacConfig.Configure(new ClientModule());
            using (var scope = container.BeginLifetimeScope())
                scope.Resolve<IApp>().Run();
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
