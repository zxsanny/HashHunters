using System.Collections.Generic;

namespace HashHunters.Autotrader.Core.DTO
{
    public enum CurrencyPair
    {
        BTC_USDT,
        ETH_USDT,
        ETH_BTC,
        ADA_BTC,
        XVG_BTC
    }

    public static class CurrencyPairConstants
    {
        public static List<CurrencyPair> MainCurrencyPairs = new List<CurrencyPair> { CurrencyPair.BTC_USDT, CurrencyPair.ETH_USDT, CurrencyPair.ETH_BTC, CurrencyPair.ADA_BTC };
    }
}
