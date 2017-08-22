using System;
using HashHunters.MinerMonitor.Common.DTO;
using HashHunters.MinerMonitor.Common.Interfaces;
using System.Linq;
using OpenHardwareMonitor.Hardware;

namespace HashHunters.MinerMonitor.Common.Providers
{
    public class HardwareInfoProvider : IHardwareInfoProvider
    {
        Computer comp;
        public HardwareInfoProvider()
        {
            comp = new Computer();
            comp.GPUEnabled = true;
            comp.Open();
        }

        public HardwareInfo GetHardware()
        {
            var gpus = comp.Hardware.Select(hw =>
            {
                hw.Update();
                hw.GetReport();

                return new GPUInfo
                {
                    Id = hw.Identifier.ToString(),
                    Name = hw.Name,
                    Temperature = (double)hw.Sensors.FirstOrDefault(x => x.SensorType == SensorType.Temperature).Value,
                    FanPercent = (double)hw.Sensors.FirstOrDefault(x => x.SensorType == SensorType.Control).Value
                };
            }).ToList();

            var machine = new HardwareInfo
            {
                MachineName = Environment.MachineName,
                MachineCurrentTime = DateTime.Now,
                GPUs = gpus
            };
            return machine;
        }
    }
}
