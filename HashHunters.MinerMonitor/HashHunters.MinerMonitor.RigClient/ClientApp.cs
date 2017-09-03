using System;
using System.Threading;
using System.Threading.Tasks;
using HashHunters.MinerMonitor.Common.DTO;
using HashHunters.MinerMonitor.Common.DTO.Enums;
using HashHunters.MinerMonitor.Common.Interfaces;
using NetworkCommsDotNet;

namespace HashHunters.MinerMonitor.RigClient
{
    public class ClientApp : IApp
    {
        private IConfigProvider ConfigProvider { get; }
        private IHardwareInfoProvider HardwareProvider { get; }
        private IEventHub EventHub { get; }

        private static readonly CancellationTokenSource CancelTokenSource = new CancellationTokenSource();
        public static CancellationToken CancelToken => CancelTokenSource.Token;

        public ClientApp(IConfigProvider configProvider, IHardwareInfoProvider hardwareProvider, IEventHub eventHub)
        {
            ConfigProvider = configProvider;
            HardwareProvider = hardwareProvider;
            EventHub = eventHub;

            var ipEndPoint = ConfigProvider.GetIpEndPoint();
            EventHub.Subscribe(e =>
            {
                NetworkComms.SendObject(e.Type.ToString(), ipEndPoint.Address.ToString(), ipEndPoint.Port, e);
            });
            EventHub.Subscribe(e =>
            {
                Console.WriteLine(e.ToString());
            });
            EventHub.Start(CancelToken);
        }

        public void Run()
        {
            while (!CancelTokenSource.IsCancellationRequested)
            {
                var hw = HardwareProvider.GetHardware();
                var he = new HardwareEvent(EventType.SensorsInfo, hw);
                EventHub.SendEvent(he);
                Thread.Sleep(30000);
            }
        }

        public void Stop()
        {
            CancelTokenSource.Cancel();
            NetworkComms.Shutdown();
        }
    }
}
