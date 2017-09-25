using System;
using System.Threading;
using HashHunters.MinerMonitor.Common.DTO;

namespace HashHunters.MinerMonitor.Common.Interfaces
{
    public interface IEventHub
    {
        void Start(CancellationToken cancelToken);
        void SendEvent(MonitorEvent e);

        void Subscribe(Action<MonitorEvent> action);
    }
}
