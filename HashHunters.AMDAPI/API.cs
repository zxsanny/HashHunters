using System.Collections.Generic;

namespace HashHunters.AMDAPI
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
    }

    public static class AMDGPUs
    {
        public static List<GPUInfo> GetInfo() => AMDInfoProvider.Get();
    }
}
