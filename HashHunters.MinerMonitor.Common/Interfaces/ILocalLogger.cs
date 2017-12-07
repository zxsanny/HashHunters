using System;

namespace HashHunters.MinerMonitor.Common.Interfaces
{
    public interface ILocalLogger
    {
        void LogError(Exception ex);
        void LogInfo(string message);
    }
}
