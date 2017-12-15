namespace HashHunters.MinerMonitor.RigClient
{
    public interface IRemoteLogger
    {
        void ServiceStart();
        void HealthCheck();
    }
}
