using HashHunters.MinerMonitor.Common.Interfaces;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;
using System.Net;

namespace HashHunters.MinerMonitor.Server
{
    public class ServerApp : IApp
    {
        IConfigProvider ConfigProvider { get; set; }

        public ServerApp(IConfigProvider configProvider)
        {
            ConfigProvider = configProvider;
        }

        public void Run()
        {
            var ipEndPoint = ConfigProvider.IPEndPoint;
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", PrintIncomingMessage);

            Connection.StartListening(ConnectionType.TCP, new IPEndPoint(IPAddress.Any, ipEndPoint.Port));

            Console.WriteLine("Server listening for TCP connection on:");
            foreach (IPEndPoint localEndPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
                Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);

            Console.WriteLine("\nPress any key to close server.");
            Console.ReadKey(true);
        }

        public void Stop()
        {
            NetworkComms.Shutdown();
        }

        private static void PrintIncomingMessage(PacketHeader header, Connection connection, string message)
        {
            Console.WriteLine($"'{message}' was received from {connection}.");
        }
    }
}
