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
                var sensorsDict = hw.Sensors.ToDictionary(x => Enum.Parse(typeof(SensorEnum), x.Name.TrimAll() + x.SensorType));
                return new GPUInfo(hw.Identifier.ToString(), hw.Name, 
                    sensorsDict[SensorEnum.GPUCoreTemperature].Value.GetValueOrDefault(), 
                    sensorsDict[SensorEnum.GPUFanControl].Value.GetValueOrDefault(),
                    sensorsDict[SensorEnum.GPUCoreLoad].Value.GetValueOrDefault());
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
