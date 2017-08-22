using HashHunters.MinerMonitor.Common.DTO;

namespace HashHunters.MinerMonitor.Common.Interfaces
{
    public interface IHardwareInfoProvider
    {
        HardwareInfo GetHardware();
    }
}
