using HashHunters.Autotrader.Core.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HashHunters.Autotrader.Core.Interfaces
{
    public interface IMarketBroker
    {
        Task<List<CandleData>> Get(CurrencyPair market, CandleInterval candleInterval, DateTime from);
        void RunPollingMarketData();
    }
}
