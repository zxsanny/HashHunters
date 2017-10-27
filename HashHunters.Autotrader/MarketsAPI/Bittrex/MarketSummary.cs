﻿using System;

namespace HashHunters.Autotrader.MarketsAPI.Bittrex
{
    public class MarketSummary
    {
        public string MarketName { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Volume { get; set; }
        public double Last { get; set; }
        public double BaseVolume { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public double OpenBuyOrders { get; set; }
        public double OpenSellOrders { get; set; }
        public double PrevDay { get; set; }
        public DateTime Created { get; set; }
    }
}
