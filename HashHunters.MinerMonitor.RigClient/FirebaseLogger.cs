using System;
using System.Net;
using FirebaseSharp.Portable;
using FirebaseSharp.Portable.Interfaces;
using HashHunters.Extensions;
using static HashHunters.MinerMonitor.RigClient.ClientApp;

namespace HashHunters.MinerMonitor.RigClient
{
    public class FirebaseLogger : IRemoteLogger
    {
        private readonly TimeSpan WAIT_TIME = TimeSpan.FromSeconds(12);

        private IFirebase Root;

        public FirebaseLogger(IConfigProvider configProvider)
        {
            var firebaseApp = new FirebaseApp(new Uri("https://rigcontrol-23592.firebaseio.com/"), configProvider.FirebaseKey);
            Root = firebaseApp.Child("Rigs").Child(Environment.MachineName);
        }

        public void HealthCheck()
        {
            Update("HealthCheck", DateTime.Now.ToNice());
        }

        public void LogMinerStats(Stats stats)
        {
            Update("MinerStats", stats);
        }

        public void ServiceStart()
        {
            FileLogger.LogInfo("Service starts");
            Update("LastStart", DateTime.Now.ToNice());

            var ip = new WebClient().DownloadString("http://bot.whatismyipaddress.com");
            Push("ServiceStarts", $"{ip} : {DateTime.Now.ToNice()}");
        }

        private void Update(string path, object value) => 
            Root.Child(path).Set(value, LogError);

        private void Push<T>(string path, T value)
        {
            var child = Root.Child(path).Push(callback: LogError);
            child.Set(value, LogError);
        }

        private void LogError(FirebaseError error)
        {
            if (error == null)
                return;
            FileLogger.LogInfo(error.Code);
            Console.WriteLine(error.Code);
        }
    }
}
