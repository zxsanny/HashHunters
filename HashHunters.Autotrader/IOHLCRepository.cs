using System;

namespace HashHunters.Autotrader
{
    public interface IOHLCRepository
    {
        void WriteTicker(string market, DateTime time, int ticker);
    }
}
