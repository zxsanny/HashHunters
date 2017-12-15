using System;
using System.Collections.Generic;

namespace HashHunters.Autotrader.MarketsAPI.Bittrex
{
    public class BittrexMarketAPI : IMarketAPI
    {
        private readonly IREST Rest;
        public string Name => "Bittrex";

        public BittrexMarketAPI(IREST rest)
        {
            Rest = rest;
            Rest.Init("https://bittrex.com/api/v1.1/");
        }

        public Ticker GetTicker(string market)
        {
            return Rest.Get<BittrexResult<Ticker>>("public/getticker", new {market}).Result;
        }

        public List<MarketSummary> GetMarketSummary(string market)
        {
            return Rest.Get<BittrexResult<List<MarketSummary>>>("public/getmarketsummary", new { market }).Result;
        }

    }
}
