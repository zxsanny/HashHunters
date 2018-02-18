using System;
using System.Collections.Generic;

namespace HashHunters.Autotrader.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Dictionary<ExchangeEnum, ExchangeKey> Exchanges { get; set; }
    }

    public class ExchangeKey
    {
        public ExchangeEnum Exchange { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }

    public enum ExchangeEnum
    {
        Bittrex = 0,
        Binance = 1,
        OKEx = 2
    }
}
