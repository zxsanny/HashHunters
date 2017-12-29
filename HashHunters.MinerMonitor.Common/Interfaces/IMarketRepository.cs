using HashHunters.MinerMonitor.Common.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HashHunters.Autotrader
{
    public interface IMarketBroker
    {
        void Start();
        Task<List<CandleData>> Get(Market market, CandleInterval candleInterval, DateTime from);
    }
}
