using System;
using System.Linq;

namespace HashHunters.MinerMonitor.Common.Extensions
{
    public static class Enum<T> where T:struct
    {
        public static T[] GetValues()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        }
    }
}
