using Autofac;
using Autofac.Core;

namespace HashHunters.MinerMonitor.RigClient
{
    public static class AutofacConfig
    {
        public static IContainer Configure(params IModule[] modules)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<JsonFileConfigProvider>().As<IConfigProvider>();
            builder.RegisterType<FirebaseLogger>().As<IRemoteLogger>().SingleInstance();
            
            foreach (var module in modules)
                builder.RegisterModule(module);
            return builder.Build();
        }
    }
}
