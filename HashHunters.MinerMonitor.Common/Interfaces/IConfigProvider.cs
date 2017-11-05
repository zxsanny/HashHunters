using System.Collections.Generic;
using System.Net;
using HashHunters.MinerMonitor.Common.DTO;

namespace HashHunters.MinerMonitor.Common.Interfaces
{
    public interface IConfigProvider
    {
        IPEndPoint IPEndPoint { get; }

        Dictionary<string, List<MinerConfig>> Miners { get; }
    }
}
