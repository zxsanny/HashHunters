using System.Runtime.InteropServices;
using Autofac;
using HashHunters.MinerMonitor.Common;
using HashHunters.MinerMonitor.Common.Interfaces;

namespace HashHunters.MinerMonitor.RigClient
{
    public class Program
    {
        private static IApp App;

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);
        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;
        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        static bool Handler(CtrlType sig)
        {
            switch (sig)
            {
                case CtrlType.CTRL_C_EVENT:
                case CtrlType.CTRL_LOGOFF_EVENT:
                case CtrlType.CTRL_SHUTDOWN_EVENT:
                case CtrlType.CTRL_CLOSE_EVENT:
                    App.Stop();
                    return true;
                default:
                    return false;
            }
        }

        static void Main(string[] args)
        {
            var container = AutofacConfig.Configure(new ClientModule());

            _handler += Handler;
            SetConsoleCtrlHandler(_handler, true);

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
