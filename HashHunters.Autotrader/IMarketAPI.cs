using System.Collections.Generic;
using HashHunters.Autotrader.MarketsAPI.Bittrex;

namespace HashHunters.Autotrader.MarketsAPI
{
    public interface IMarketAPI
    {
        string Name { get; }

        Ticker GetTicker(string market);
        List<MarketSummary> GetMarketSummary(string market);
        
        //PutOrder();
    }
}
