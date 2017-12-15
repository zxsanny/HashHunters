using HashHunters.MinerMonitor.Common.Extensions;
using StackExchange.Redis;
using System;

namespace HashHunters.Autotrader
{
    public class OHLCRedisRepository : IOHLCRepository
    {
        ConnectionMultiplexer Redis;
        IDatabase Db;

        public OHLCRedisRepository()
        {
            Redis = ConnectionMultiplexer.Connect("localhost");
            Db = Redis.GetDatabase();
        }

        public void WriteTicker(string market, DateTime time, int ticker)
        {
            Db.StringSet("some", "value");
            //Db.SetAdd(time.ToUnixTime(), ticker);

        }
    }
}
