﻿using Autofac;
using HashHunters.MinerMonitor.Common;
using HashHunters.MinerMonitor.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace HashHunters.SafeTemperature
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

            var container = AutofacConfig.Configure(new WindowsFormsModule());
            using (var scope = container.BeginLifetimeScope())
                scope.Resolve<IApp>().Run();
        }
    }

    public class WindowsFormsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<WindowsApp>().As<IApp>();
            builder.RegisterType<FormInputOutput>().As<IIO>();
            builder.RegisterType<MainForm>().SingleInstance();
        }

    }

    public class WindowsApp : IApp
    {
        IConfigProvider ConfigProvider;
        IHardwareInfoProvider HardwareProvider;
        IIO IO;

        public WindowsApp(IConfigProvider configProvider, IHardwareInfoProvider hardwareProvider, IIO io)
        {
            ConfigProvider = configProvider;
            HardwareProvider = hardwareProvider;
            IO = io;
        }

        public void Run()
        {
            RunMonitor();
            IO.ViewForm();
        }

        public void Stop()
        {
        }

        void RunMonitor()
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                while (true)
                {
                    var maxTemp = IO.GetMaxTemperature();
                    var hw = HardwareProvider.GetHardware();
                    IO.SwitchAlert(hw.GPUInfos.Any(x => x.Temperature > maxTemp));
                    Thread.Sleep(2000);
                }
            });
        }
    }
}