using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HashHunters.MinerMonitor.RigClient.DTO;
using Newtonsoft.Json;

namespace HashHunters.MinerMonitor.RigClient
{
    public interface IApp
    {
        void Run();
        void Stop();
    }

    public class ClientApp : IApp
    {
        IConfigProvider ConfigProvider;
        IRemoteLogger Logger;
        
        private static readonly CancellationTokenSource CancelTokenSource = new CancellationTokenSource();
        public static CancellationToken CancelToken => CancelTokenSource.Token;

        private readonly Dictionary<string, MinerConfig> CurrentMiners = new Dictionary<string, MinerConfig>();

        public ClientApp(IConfigProvider configProvider, IRemoteLogger logger)
        {
            ConfigProvider = configProvider;
            Logger = logger;
        }

        public void Run()
        {
            Task.Run(() => 
            {
                Logger.ServiceStart();
                while (true)
                {
                    Logger.HealthCheck();
                    Logger.LogMinerStats(GetStats());
                    MinersCheck();
                    Thread.Sleep(20000);
                }
            }, CancelToken);
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
                    MinInterval = mc.MinimalActiveInterval
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
                FileLogger.LogInfo($"{p.ProcessName} [PID: {p.Id}] stopped!");
                p.Kill();
            }
        }

        private static void RunMiner(string programName, MinerConfig minerConfig)
        {
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
            }
            catch (Exception e)
            {
                FileLogger.LogInfo($"Starting {programName} failed! Error: {Environment.NewLine}{e}");
            }
        }

        public void Stop()
        {
            FileLogger.LogInfo("Service stops, Cancel token activated");
            CancelTokenSource.Cancel();
        }


        public class Stats
        {
            // Miner stats
            public string Version;
            public int TotalHashrate;
            public List<int> Hashrates;
            public TimeSpan Uptime;

            // Pool stats
            public int Accepted;
            public int Rejected;

            // Exception
            public Exception Exception;

            public Stats()
            {
                Hashrates = new List<int>();
                Exception = null;
            }
        }

        class EthMonJsonTemplate
        {
            public int id { get; set; }
            public string error { get; set; }
            public List<string> result { get; set; }
        }


        private Stats GetStats()
        {
            var stats = new Stats();
            try
            {
                using (var client = new TcpClient())
                {
                    if (client.ConnectAsync("localhost", 3333).Wait(5000))
                    {
                        var serverStream = client.GetStream();
                        byte[] bytes = Encoding.ASCII.GetBytes("{\"id\":0,\"jsonrpc\":\"2.0\",\"method\":\"miner_getstat1\"}");
                        serverStream.Write(bytes, 0, bytes.Length);
                        serverStream.Flush();

                        var inStream = new byte[client.ReceiveBufferSize];
                        serverStream.Read(inStream, 0, client.ReceiveBufferSize);
                        var _returndata = Encoding.ASCII.GetString(inStream);

                        if (_returndata.Length == 0)
                            throw new Exception("Invalid data");

                        var result = JsonConvert.DeserializeObject<EthMonJsonTemplate>(_returndata);

                        stats.Version = result.result[0]; // Version
                        stats.Uptime = TimeSpan.FromMinutes(int.Parse(result.result[1]));

                        var minerStats = result.result[2].Split(';');
                        stats.TotalHashrate = int.Parse(minerStats[0]);
                        stats.Accepted = Int32.Parse(minerStats[1]);
                        stats.Rejected = Int32.Parse(minerStats[2]);

                        // Dual Stats
                        stats.Hashrates = result.result[3].Split(';')
                            .Select(x => int.Parse(x)).ToList(); // ETH Hashrates
                        
                        // Close socket
                        client.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                stats.Exception = ex;
                FileLogger.LogError(ex);
            }

            return stats;
        }
    }
}
