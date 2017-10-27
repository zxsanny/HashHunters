using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HashHunters.MinerMonitor.Common.DTO;
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

        private const string ETHAddress = "0xd70921f415d48f2af3b005c5ec2c2279df7a94a2";

        private readonly List<Miner> Miners = new List<Miner>
        {
            new Miner("MSIAfterburner"),
            new Miner("EthDcrMiner64", $"-dbg -1 -epool eu1.ethermine.org:4444 -ewal {ETHAddress}.{Environment.MachineName} -epsw x -mode 0 -ftime 10 -dpool dcr.suprnova.cc:3252 " +
                $"-dwal HashHunters.{Environment.MachineName} -dpsw HashHunters"),
            new Miner("xmr-stak-cpu-notls", "", "C:\\Mining\\XMR\\CPU")
        };
        
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
            Task.Run((Action)MainThread, CancelToken);
        }

        public void MainThread()
        {
            while (true)
            {
                var processes = Process.GetProcesses();
                foreach (var miner in Miners)
                {
                    Console.WriteLine($"Ensure {miner.ProgramName} in running");
                    if (processes.All(x => x.ProcessName != miner.ProgramName))
                    {
                        Console.WriteLine($"{miner.ProgramName} is not running! Starting {miner.ProgramName}...");
                        //EventHub.SendEvent(he);
                        try
                        {
                            var process = new Process();
                            process.StartInfo = new ProcessStartInfo
                            {
                                FileName = miner.ProgramName,
                                Arguments = miner.Parameters,
                                WorkingDirectory = miner.ProgramFolder
                            };
                            process.Start();
                            process.PriorityClass = ProcessPriorityClass.High;
                            Console.WriteLine($"{miner.ProgramName} started!");
                            //EventHub.SendEvent(he);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Starting {miner.ProgramName} failed! Error: {Environment.NewLine}{e}");
                            //EventHub.SendEvent(he);
                        }
                    }
                }
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
