using System;

namespace HashHunters.MinerMonitor.Extensions
{
    public static class DateTimeExtensions
    {
        readonly static DateTime unixBeginTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static string ToNice(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static long ToUnixTime(this DateTime datetime)
        {
            return (long)(datetime - unixBeginTime).TotalSeconds;
        }
    }
}
