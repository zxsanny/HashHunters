using Autofac;
using HashHunters.MinerMonitor.Common;
using HashHunters.MinerMonitor.Common.Interfaces;

namespace HashHunters.MinerMonitor.Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            var container = AutofacConfig.Configure(new ServerModule());
            using (var scope = container.BeginLifetimeScope())
                scope.Resolve<IApp>().Run();
        }
    }

    public class ServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<ServerApp>().As<IApp>();
        }
    }
}
