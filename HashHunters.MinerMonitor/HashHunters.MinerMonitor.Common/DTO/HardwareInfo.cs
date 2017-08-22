using System;
using System.Collections.Generic;

namespace HashHunters.MinerMonitor.Common.DTO
{
    public class HardwareInfo
    {
        public string MachineName { get; set; }
        public DateTime MachineCurrentTime{ get; set;}
        public List<GPUInfo> GPUs { get; set; }
    }

    public class GPUInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Temperature { get; set; }
        public double FanPercent { get; set; }
    }
}
