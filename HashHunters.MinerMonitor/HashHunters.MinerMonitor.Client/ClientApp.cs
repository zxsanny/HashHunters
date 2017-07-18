using HashHunters.MinerMonitor.Common.Interfaces;
using NetworkCommsDotNet;
using System;

namespace HashHunters.MinerMonitor.Client
{
    public class ClientApp : IApp
    {
        IConfigProvider ConfigProvider { get; set; }

        public ClientApp(IConfigProvider configProvider)
        {
            ConfigProvider = configProvider;
        }

        public void Run()
        {
            var ipEndPoint = ConfigProvider.GetIpEndPoint();
            int loopCounter = 1;
            while (true)
            {
                string msg = "This is message #" + loopCounter;
                Console.WriteLine("Sending message to server saying '" + msg + "'");

                NetworkComms.SendObject("Message", ipEndPoint.Address.ToString(), ipEndPoint.Port, msg);

                Console.WriteLine("\nPress q to quit or any other key to send another message.");
                if (Console.ReadKey(true).Key == ConsoleKey.Q) break;
                else loopCounter++;
            }
            NetworkComms.Shutdown();
        }
    }
}
