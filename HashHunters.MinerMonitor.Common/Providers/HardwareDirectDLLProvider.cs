using System;
using System.Collections.Generic;
using System.Linq;
using HashHunters.AMDAPI;
using HashHunters.MinerMonitor.Common.DTO;
using HashHunters.MinerMonitor.Common.Interfaces;
using HashHunters.NVidiaAPI;
using GPUInfo = HashHunters.MinerMonitor.Common.DTO.GPUInfo;

namespace HashHunters.MinerMonitor.Common.Providers
{
    public class HardwareDirectDLLProvider : IHardwareInfoProvider
    {
        public HardwareInfo GetHardware()
        {
            var nvGPUs = NVidiaGPUs.GetInfo().Select(x => new GPUInfo(x)).ToList();
            var amdGPUs = AMDGPUs.GetInfo().Select(x => new GPUInfo(x)).ToList();

            var gpus = new List<GPUInfo>(amdGPUs);
            gpus.AddRange(nvGPUs);

            return new HardwareInfo
            {
                Name = Environment.MachineName,
                Time = DateTime.UtcNow,
                GPUInfos = gpus
            };
        }

    }
}
