using System.Collections.Generic;
using HashHunters.Autotrader.MarketsAPI.Bittrex;
using HashHunters.Autotrader.MarketsAPI.Yobit;

namespace HashHunters.Autotrader.MarketsAPI
{
    public interface IMarketAPI
    {
        string Name { get; }

        Ticker GetTicker(string market);
        YobitTicker GetTickerYobit(string market);
        List<MarketSummary> GetMarketSummary(string market);
        
        //PutOrder();
    }
}
