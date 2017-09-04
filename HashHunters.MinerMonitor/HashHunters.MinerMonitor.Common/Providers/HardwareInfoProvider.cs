using System;
using HashHunters.MinerMonitor.Common.DTO;
using HashHunters.MinerMonitor.Common.Interfaces;
using System.Linq;
using HashHunters.MinerMonitor.Common.Enums;
using HashHunters.MinerMonitor.Common.Extensions;
using OpenHardwareMonitor.Hardware;

namespace HashHunters.MinerMonitor.Common.Providers
{
    public class HardwareInfoProvider : IHardwareInfoProvider
    {
        private readonly Computer comp;
        public HardwareInfoProvider()
        {
            comp = new Computer { GPUEnabled = true };
            comp.Open();
        }

        public HardwareInfo GetHardware()
        {
            var gpus = comp.Hardware.Select(hw =>
            {
                hw.Update();
                hw.GetReport();
                var temp = hw.Sensors?.FirstOrDefault(x => x.SensorType == SensorType.Temperature && x.Name == "GPUCore")?.Value;
                var fanPercent = hw.Sensors?.FirstOrDefault(x => x.SensorType == SensorType.Control && x.Name == "GPUFan")?.Value;
                var load = hw.Sensors?.FirstOrDefault(x => x.SensorType == SensorType.Load && x.Name == "GPUCore")?.Value;

                return new GPUInfo(hw.Identifier.ToString(), hw.Name, temp.GetValueOrDefault(), fanPercent.GetValueOrDefault(), load.GetValueOrDefault());
            }).ToList();

            var machine = new HardwareInfo
            {
                MachineName = Environment.MachineName,
                MachineCurrentTime = DateTime.Now,
                GPUInfos = gpus
            };
            return machine;
        }
    }
}
