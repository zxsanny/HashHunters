using HashHunters.MinerMonitor.Common.Interfaces;
using System.Configuration;
using System.Net;

namespace HashHunters.MinerMonitor.Common.Providers
{
    public class ConfigProvider : IConfigProvider
    {
        public IPEndPoint GetIpEndPoint()
        {
            var ip = ConfigurationManager.AppSettings["ServerIP"];
            var port = ConfigurationManager.AppSettings["ServerPort"];
            return new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
        }
    }
}
