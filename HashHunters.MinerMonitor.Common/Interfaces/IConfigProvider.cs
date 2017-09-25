using System.Net;

namespace HashHunters.MinerMonitor.Common.Interfaces
{
    public interface IConfigProvider
    {
        IPEndPoint GetIpEndPoint();
    }
}
