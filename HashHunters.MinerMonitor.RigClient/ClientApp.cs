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
        private ILogger Logger { get; }

        private static readonly CancellationTokenSource CancelTokenSource = new CancellationTokenSource();
        public static CancellationToken CancelToken => CancelTokenSource.Token;

        private readonly Dictionary<string, MinerConfig> CurrentMiners = new Dictionary<string, MinerConfig>();

        public ClientApp(IConfigProvider configProvider, IHardwareInfoProvider hardwareProvider, IEventHub eventHub, ILogger logger)
        {
            ConfigProvider = configProvider;
            HardwareProvider = hardwareProvider;
            EventHub = eventHub;
            Logger = logger;

            var ipEndPoint = ConfigProvider.IPEndPoint;
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
                Logger.HealthCheck();
                MinersCheck();
                Thread.Sleep(20000);
            }
        }

        private void MinersCheck()
        {
            var allProcesses = Process.GetProcesses();
            foreach (var miner in ConfigProvider.Miners)
            {
                var processes = allProcesses.Where(p => p.ProcessName.ToLowerInvariant() == miner.Key.ToLowerInvariant()).ToList();

                var minerConfig = miner.Value.Select(mc => new
                {
                    Config = mc,
                    MinInterval = mc.GetCurrentMinimumInterval()
                })
                .Where(x => x.MinInterval != null)
                .OrderBy(x => x.MinInterval).FirstOrDefault()?.Config;

                if (minerConfig != null)
                {
                    if (!processes.Any() || !CurrentMiners.ContainsKey(miner.Key) || CurrentMiners[miner.Key] != minerConfig)
                    {
                        StopMiner(processes);
                        CurrentMiners.Remove(miner.Key);
                        CurrentMiners.Add(miner.Key, minerConfig);

                        RunMiner(miner.Key, minerConfig);
                    }
                }
                else
                    StopMiner(processes);
            }
        }

        private static void StopMiner(IEnumerable<Process> processes)
        {
            foreach (var p in processes)
            {
                Console.WriteLine($"{p.ProcessName} [PID: {p.Id}] stopped!");
                p.Kill();
            }
        }

        private static void RunMiner(string programName, MinerConfig minerConfig)
        {
            //EventHub.SendEvent(he);
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = programName,
                        Arguments = minerConfig.Parameters,
                        WorkingDirectory = minerConfig.ProgramFolder
                    }
                };
                process.Start();
                process.PriorityClass = ProcessPriorityClass.High;
                Console.WriteLine($"{programName} started!");
                //EventHub.SendEvent(he);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Starting {programName} failed! Error: {Environment.NewLine}{e}");
                //EventHub.SendEvent(he);
            }
        }

        public void Stop()
        {
            CancelTokenSource.Cancel();
            NetworkComms.Shutdown();
        }
    }
}
