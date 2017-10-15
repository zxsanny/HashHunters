using System.ServiceProcess;
using Autofac;
using Autofac.Core;

namespace HashHunters.MinerMonitor.RigService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<RigService>();

            HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.Service<MyService>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsing(() => new MyService());
                    serviceConfigurator.WhenStarted(myService => myService.Start());
                    serviceConfigurator.WhenStopped(myService => myService.Stop());
                });

                hostConfigurator.RunAsLocalSystem();
                hostConfigurator.SetDescription("MyService using Topshelf");
                hostConfigurator.SetDisplayName("MyService");
                hostConfigurator.SetServiceName("MyService");
            });
        }
    }
}
