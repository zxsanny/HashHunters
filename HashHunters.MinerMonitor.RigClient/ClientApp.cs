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

            var ipEndPoint = ConfigProvider.IPEndPoint;
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
            public string TotalHashrate;
            public string TotalDualHashrate;
            public List<string> hashrates;
            public List<string> DcrHashrates;
            public List<string> Temps;
            public List<string> FanSpeeds;
            public Boolean IsOnline;
            public string Uptime;

            // Exception
            public Exception Exception;

            // Pool stats
            public int Accepted;
            public int Rejected;
            public int DualAccepted;
            public int DualRejected;
        }

        class EthMonJsonTemplate
        {
            public int id { get; set; }
            public string error { get; set; }
            public List<string> result { get; set; }
        }


        private Stats GetStats()
        {
            var stats = new Stats()
            {
                IsOnline = false,
                Exception = null,
                Uptime = "",
                Version = "",
                hashrates = new List<string>(),
                DcrHashrates = new List<string>(),
                Temps = new List<string>(),
                FanSpeeds = new List<string>(),
                DualAccepted = 0,
                DualRejected = 0,
                TotalDualHashrate = ""
            };

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
                        stats.Uptime = result.result[1]; // Uptime

                        string[] minerStats = result.result[2].Split(';');
                        stats.TotalHashrate = minerStats[0];
                        stats.Accepted = Int32.Parse(minerStats[1]);
                        stats.Rejected = Int32.Parse(minerStats[2]);

                        // Dual Stats
                        string[] dualStats = result.result[4].Split(';');
                        stats.TotalDualHashrate = dualStats[0];
                        stats.DualAccepted = Int32.Parse(dualStats[1]);
                        stats.DualRejected = Int32.Parse(dualStats[2]);

                        string[] hashrates = result.result[3].Split(';'); // ETH Hashrates

                        for (int i = 0; i < hashrates.Length; i++)
                        {
                            stats.hashrates.Add(hashrates[i]);
                        }

                        string[] dcrHashrates = result.result[5].Split(';'); // DCR Hashrates

                        for (int i = 0; i < dcrHashrates.Length; i++)
                        {
                            stats.DcrHashrates.Add(dcrHashrates[i]);
                        }

                        // Temps and fan speeds
                        string[] temp = result.result[6].Split(';');
                        try
                        {
                            int tempRow = 0;
                            for (int i = 0; i < temp.Length; i++)
                            {
                                stats.Temps.Add(temp[i]);
                                stats.FanSpeeds.Add(temp[i + 1]);
                                i++;
                                tempRow++;
                            }
                        }
                        catch (Exception ex)
                        {
                            FileLogger.LogError(ex);
                        }

                        // Close socket
                        client.Close();
                        
                        stats.IsOnline = true; // Online
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
