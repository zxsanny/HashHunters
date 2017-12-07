namespace HashHunters.MinerMonitor.Common.Interfaces
{
    public interface IRemoteLogger
    {
        void ServiceStart();
        void HealthCheck();
    }
}
