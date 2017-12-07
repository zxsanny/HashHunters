using HashHunters.MinerMonitor.Common.Extensions;
using HashHunters.MinerMonitor.Common.Interfaces;
using System;
using System.IO;
using System.Text;

namespace HashHunters.MinerMonitor.Common
{
    public class FileLogger : ILocalLogger
    {
        const string LOG_FILE = "hashhunters.log";

        public void LogError(Exception ex)
        {
            var s = new StringBuilder("-------------------------------------------");
            s.AppendLine().AppendLine($"{DateTime.Now.ToNice()}: {ex.Message}");
            s.AppendLine(ex.StackTrace);
            s.AppendLine("-------------------------------------------");
            File.AppendAllText(LOG_FILE, s.ToString());
        }

        public void LogInfo(string message)
        {
            var s = new StringBuilder("-------------------------------------------");
            s.AppendLine().AppendLine($"{DateTime.Now.ToNice()}: {message}");
            s.AppendLine("-------------------------------------------");
            File.AppendAllText(LOG_FILE, s.ToString());
        }
    }
}
