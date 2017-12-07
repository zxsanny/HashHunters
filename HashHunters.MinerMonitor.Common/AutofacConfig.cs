using Autofac;
using Autofac.Core;
using HashHunters.MinerMonitor.Common.Interfaces;
using HashHunters.MinerMonitor.Common.Providers;

namespace HashHunters.MinerMonitor.Common
{
    public static class AutofacConfig
    {
        public static IContainer Configure(params IModule[] modules)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<JsonFileConfigProvider>().As<IConfigProvider>();
            builder.RegisterType<DirectDllInfoProvider>().As<IHardwareInfoProvider>().SingleInstance();
            builder.RegisterType<EventHub>().As<IEventHub>().SingleInstance();
            builder.RegisterType<FirebaseLogger>().As<IRemoteLogger>().SingleInstance();
            builder.RegisterType<FileLogger>().As<ILocalLogger>().SingleInstance();

            foreach (var module in modules)
                builder.RegisterModule(module);
            return builder.Build();
        }
    }
}
