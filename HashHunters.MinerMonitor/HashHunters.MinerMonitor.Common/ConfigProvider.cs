using HashHunters.MinerMonitor.Common.Interfaces;
using System.Configuration;
using System.Net;

namespace HashHunters.MinerMonitor.Common
{
    public class ConfigProvider : IConfigProvider
    {
        public IPEndPoint GetIpEndPoint()
        {
            var ip = ConfigurationManager.AppSettings["IP"];
            var port = ConfigurationManager.AppSettings["Port"];
            return new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
        }
    }
}
