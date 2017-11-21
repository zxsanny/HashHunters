using System;

namespace HashHunters.MinerMonitor.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToNice(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
