using System;
using Firebase.Database;
using HashHunters.MinerMonitor.Common.Interfaces;
using System.Threading.Tasks;
using Firebase.Database.Query;
using HashHunters.MinerMonitor.Common.Extensions;

namespace HashHunters.MinerMonitor.Common
{
    public class FirebaseLogger : IRemoteLogger
    {
        ILocalLogger LocalLogger;

        private readonly TimeSpan WAIT_TIME = TimeSpan.FromSeconds(12);

        private readonly FirebaseClient FirebaseClient;
        private ChildQuery Root => FirebaseClient.Child("Rigs").Child(Environment.MachineName);

        public FirebaseLogger(IConfigProvider configProvider, ILocalLogger localLogger)
        {
            FirebaseClient = new FirebaseClient("https://rigcontrol-23592.firebaseio.com/",
                new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(configProvider.FirebaseKey) });
            LocalLogger = localLogger;
        }

        public void HealthCheck()
        {
            FirebasePut("HealthCheck", DateTime.Now.ToNice());
        }

        public void ServiceStart()
        {
            LocalLogger.LogInfo("Service starts");
            FirebasePut("LastStart", DateTime.Now.ToNice());
            FirebasePost("ServiceStarts", DateTime.Now.ToNice());
        }

        private void FirebasePut<T>(string path, T value)
        {
            try
            {
                Root.Child(path).PutAsync(value).Wait(WAIT_TIME);
                throw new Exception("ololo PUT");
            }
            catch (Exception e)
            {
                LocalLogger.LogError(e);
                Console.WriteLine(e);
            }
        }

        private void FirebasePost<T>(string path, T value)
        {
            try
            {
                Root.Child(path).PostAsync(value).Wait(WAIT_TIME);
                throw new Exception("ololo POST");
            }
            catch (Exception e)
            {
                LocalLogger.LogError(e);
                Console.WriteLine(e);
            }
        }
    }
}
