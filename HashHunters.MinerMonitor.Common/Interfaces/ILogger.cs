namespace HashHunters.MinerMonitor.Common.Interfaces
{
    public interface ILogger
    {
        void ServiceStart();
        void HealthCheck();
    }
}
