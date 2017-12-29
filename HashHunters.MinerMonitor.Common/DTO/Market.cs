using System.Collections.Generic;

namespace HashHunters.MinerMonitor.Common.DTO
{
    public enum Market
    {
        BTC_USDT,
        ETH_USDT,
        ETH_BTC,
        ADA_BTC,
        XVG_BTC
    }

    public static class MarketConstants
    {
        public static List<Market> MainMarkets = new List<Market> { Market.BTC_USDT, Market.ETH_USDT, Market.ETH_BTC, Market.ADA_BTC };
    }
}
