using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HashHunters.MinerMonitor.RigClient.DTO;
using Newtonsoft.Json;

namespace HashHunters.MinerMonitor.RigClient
{
    public class JsonFileConfigProvider : IConfigProvider
    {
        private const string SETTINGS_FILE = "settings.json";
        private const string ETH_ADDRESS = "0xd70921f415d48f2af3b005c5ec2c2279df7a94a2";

        private RigSettings RigSettings;

        public JsonFileConfigProvider()
        {
            EnsureConfig();
        }

        private void EnsureConfig()
        {
            var path = Path.Combine(Environment.CurrentDirectory, SETTINGS_FILE);
            if (!File.Exists(path))
            {
                var rigSettings = new RigSettings
                {
                    ServerIP = "127.0.0.1",
                    ServerPort = "80",
                    FirebaseKey = "OeJkpfagiVD7h5sNJrZxia5cBQ6p8Al7C0UQbSVB",
                    Rigs = new List<RigConfig>
                    {
                        new RigConfig
                        {
                            MachineName = "HHWORKER01",
                            MinerConfigs = new Dictionary<string, List<MinerConfig>>
                            {
                                { "MSIAfterburner", new List<MinerConfig> { new MinerConfig() } },
                                { "EthDcrMiner64", new List<MinerConfig>
                                    {
                                        new MinerConfig($"-dbg -1 -epool eu1.ethermine.org:4444 -ewal {ETH_ADDRESS}.HHWORKER01 " +
                                                            "-epsw x -mode 0 -ftime 10 -dpool dcr.suprnova.cc:3252 -dwal HashHunters.HHWORKER01 -dpsw HashHunters"),

                                        new MinerConfig($"-dbg -1 -epool eu1.ethermine.org:4444 -ewal {ETH_ADDRESS}.HHWORKER01 " +
                                            "-di 12 -epsw x -mode 0 -ftime 10 -dpool dcr.suprnova.cc:3252 -dwal HashHunters.HHWORKER01 -dpsw HashHunters", "",
                                            new List<TimeInterval>
                                            {
                                                new TimeInterval(new TimeSpan(8, 0, 0), new TimeSpan(10, 0, 0)),
                                                new TimeInterval(new TimeSpan(18, 0, 0), new TimeSpan(21, 0, 0)),
                                            })
                                    }
                                },
                                { "xmr-stak-cpu-notls", new List<MinerConfig> {new MinerConfig("", "C:\\Mining\\XMR\\CPU") } }
                            }
                        },
                        new RigConfig
                        {
                            MachineName = "HHWORKER02",
                            MinerConfigs = new Dictionary<string, List<MinerConfig>>
                            {
                                { "MSIAfterburner", new List<MinerConfig> { new MinerConfig() } },
                                { "EthDcrMiner64", new List<MinerConfig>
                                    {
                                        new MinerConfig($"-dbg -1 -epool eu1.ethermine.org:4444 -ewal {ETH_ADDRESS}.HHWORKER02 " +
                                                        "-epsw x -mode 0 -ftime 10 -dpool dcr.suprnova.cc:3252 -dwal HashHunters.HHWORKER02 -dpsw HashHunters")
                                    }
                                },
                                { "xmr-stak-cpu-notls", new List<MinerConfig> { new MinerConfig("", "C:\\Mining\\XMR\\CPU") } }
                            }
                        }
                    }
                };
                File.WriteAllText(path, JsonConvert.SerializeObject(rigSettings, Formatting.Indented));
            }

            RigSettings = JsonConvert.DeserializeObject<RigSettings>(File.ReadAllText(path));
        }

        public string FirebaseKey
        {
            get
            {
                EnsureConfig();
                return RigSettings.FirebaseKey;
            }
        }

        public IPEndPoint IPEndPoint
        {
            get
            {
                EnsureConfig();
                return new IPEndPoint(IPAddress.Parse(RigSettings.ServerIP), int.Parse(RigSettings.ServerPort));
            }
        }

        public Dictionary<string, List<MinerConfig>> Miners
        {
            get
            {
                EnsureConfig();
                return RigSettings.Rigs.FirstOrDefault(x => x.MachineName.ToUpper() == Environment.MachineName.ToUpper())?.MinerConfigs;
            }
        }
    }
}