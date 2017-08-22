using HashHunters.MinerMonitor.Common.Interfaces;
using NetworkCommsDotNet;
using System;

namespace HashHunters.MinerMonitor.Client
{
    public class ClientApp : IApp
    {
        IConfigProvider ConfigProvider { get; set; }
        IHardwareInfoProvider MachineInfoProvider { get; set; }

        public ClientApp(IConfigProvider configProvider, IHardwareInfoProvider machineInfoProvider)
        {
            ConfigProvider = configProvider;
            MachineInfoProvider = machineInfoProvider;
        }

        public void Run()
        {
            var ipEndPoint = ConfigProvider.GetIpEndPoint();
            
            var hardware = MachineInfoProvider.GetHardware();

            NetworkComms.SendObject("HardwareInfo", ipEndPoint.Address.ToString(), ipEndPoint.Port, hardware);

            

            NetworkComms.Shutdown();
        }
    }
}
