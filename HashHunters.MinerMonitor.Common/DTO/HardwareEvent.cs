using System;
using System.Collections.Generic;
using System.Linq;
using HashHunters.MinerMonitor.Common.DTO.Enums;

namespace HashHunters.MinerMonitor.Common.DTO
{
    public class GPUInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Temperature { get; set; }
        public double FanPercent { get; set; }
        public double Load { get; set; }

        public GPUInfo(string id, string name, double temperature, double fanPercent, double load)
        {
            Id = id;
            Name = name;
            Temperature = temperature;
            FanPercent = fanPercent;
            Load = load;
        }

        public GPUInfo(AMDAPI.GPUInfo amdInfo)
        {
            Id = amdInfo.Id;
            Name = amdInfo.Name;
            Temperature = amdInfo.Temperature;
            FanPercent = amdInfo.FanPercent;
            Load = amdInfo.Load;
        }

        public GPUInfo(NVidiaAPI.GPUInfo nvInfo)
        {
            Id = nvInfo.Id;
            Name = nvInfo.Name;
            Temperature = nvInfo.Temperature;
            FanPercent = nvInfo.FanPercent;
            Load = nvInfo.Load;
        }
    }

    public class HardwareInfo
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public List<GPUInfo> GPUInfos { get; set; }
    }

    public class HardwareEvent : MonitorEvent
    {
        public HardwareInfo Info { get; }

        public HardwareEvent(EventType type, HardwareInfo info) : base(type)
        {
            Info = info;
        }

        public override string ToString()
        {
            var gpuInfos = string.Join(",", Info.GPUInfos.Select(x =>
            {
                var name = $"#{ x.Id.Split('/').LastOrDefault()} {x.Name}:";
                return $"{name} {x.Temperature}C fan:{x.FanPercent}% load:{x.Load}%";
            }).ToArray());

            return $"{Info.Time:yy-MM-dd HH:mm:ss} {Info.Name}: {gpuInfos}";
        }
    }
}
