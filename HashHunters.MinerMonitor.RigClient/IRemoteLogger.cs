namespace HashHunters.MinerMonitor.RigClient
{
    public interface IRemoteLogger
    {
        void ServiceStart();
        void HealthCheck();
        void LogMinerStats(ClientApp.Stats stats);
    }
}
