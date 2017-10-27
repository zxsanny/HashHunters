using System;
using System.Collections.Generic;
using System.Linq;
using HashHunters.Autotrader.MarketsAPI.Bittrex;

namespace HashHunters.Autotrader.MarketsAPI.Yobit
{
    public class YobitMarketAPI : IMarketAPI
    {
        private readonly IREST Rest;

        public string Name => "Yobit";

        public YobitMarketAPI(IREST rest)
        {
            Rest = rest;
            Rest.Init("https://yobit.net/api/3/");
        }

        public YobitTicker GetTickerYobit(string market)
        {
            var res = Rest.Get<Dictionary<string, YobitTicker>>($"ticker/{market}").Values.FirstOrDefault();
            return res;
        }

        public Ticker GetTicker(string market)
        {
            throw new NotImplementedException();
        }

        public List<MarketSummary> GetMarketSummary(string market)
        {
            throw new NotImplementedException();
        }
    }
}
