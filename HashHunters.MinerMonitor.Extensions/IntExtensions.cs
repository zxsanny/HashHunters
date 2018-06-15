namespace HashHunters.MinerMonitor.Extensions
{
    public static class IntExtensions
    {
        public static int ToInt(this string s)
        {
            var res = 0;
            int.TryParse(s, out res);
            return res;
        }
    }
}
