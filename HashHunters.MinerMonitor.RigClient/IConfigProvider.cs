using System.Collections.Generic;
using System.Net;
using HashHunters.MinerMonitor.RigClient.DTO;

namespace HashHunters.MinerMonitor.RigClient
{
    public interface IConfigProvider
    {
        string FirebaseKey { get; }
        IPEndPoint IPEndPoint { get; }

        Dictionary<string, List<MinerConfig>> Miners { get; }
    }
}
