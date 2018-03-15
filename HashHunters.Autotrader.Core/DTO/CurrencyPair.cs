using System.Collections.Generic;

namespace HashHunters.Autotrader.Core.DTO
{
    public enum Currency
    {
        USDT = 0,
        BTC = 1,
        ETH = 2,
        ADA = 3,

    }

    public struct CurrencyPair
    {
        public Currency Currency;
        public Currency BaseCurrency;

        public override string ToString()
        {
            return $"{Currency}_{BaseCurrency}";
        }
    }
}
