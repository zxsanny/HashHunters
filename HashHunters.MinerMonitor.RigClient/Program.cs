using System.Diagnostics;
using Autofac;
using HashHunters.MinerMonitor.Common;
using HashHunters.MinerMonitor.Common.Interfaces;
using Topshelf;

namespace HashHunters.MinerMonitor.RigClient
{
    public class Program
    {
        private static IApp App;

        static void Main(string[] args)
        {
            var container = AutofacConfig.Configure(new ClientModule());
            HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.Service<IApp>(s =>
                {
                    s.ConstructUsing(() => container.Resolve<IApp>());
                    s.WhenStarted(clientApp => clientApp.Run());
                    s.WhenStopped(clientApp => clientApp.Stop());
                });

                hostConfigurator.RunAsLocalService();
                hostConfigurator.DependsOnEventLog().BeforeInstall(settings =>
                    {
                        if (!EventLog.SourceExists(settings.ServiceName))
                            EventLog.CreateEventSource(settings.ServiceName, "RigService");
                    }).UseAssemblyInfoForServiceInfo();

                hostConfigurator.SetDescription("RigService");
                hostConfigurator.SetDisplayName("RigService");
                hostConfigurator.SetServiceName("RigService");
            });
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
