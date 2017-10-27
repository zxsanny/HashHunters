using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using HashHunters.Autotrader.MarketsAPI;

namespace HashHunters.Autotrader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ContainerBuilder();
            builder.RegisterType<Form1>().SingleInstance();
            builder.RegisterType<REST>().As<IREST>();

            var types = Assembly.GetExecutingAssembly().DefinedTypes.Where(t => typeof(IMarketAPI).IsAssignableFrom(t) && t != typeof(IMarketAPI)).ToList();
            foreach (var t in types)
                builder.RegisterType(t).As<IMarketAPI>();
                

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
                Application.Run(scope.Resolve<Form1>());
        }
    }
}
