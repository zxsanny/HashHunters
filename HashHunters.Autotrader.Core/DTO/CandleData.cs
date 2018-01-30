using System;

namespace HashHunters.Autotrader.Core.DTO
{
    public class CandleData
    {
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal BaseVolume { get; set; }

        public override string ToString() =>
            $"{Open}|{High}|{Low}|{Close}|{Volume}|{BaseVolume}";

        public CandleData(){}

        public CandleData(string data)
        {
            var values = data.Split('|');
            Open = Convert.ToDecimal(values[0]);
            High = Convert.ToDecimal(values[1]);
            Low = Convert.ToDecimal(values[2]);
            Close = Convert.ToDecimal(values[3]);
            Volume = Convert.ToDecimal(values[4]);
            BaseVolume = Convert.ToDecimal(values[5]);
        }
    }
}
