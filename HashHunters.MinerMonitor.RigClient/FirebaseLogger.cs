using System;
using System.Threading.Tasks;
using HashHunters.MinerMonitor.Common.Extensions;
using Firebase.Database;
using Firebase.Database.Query;

namespace HashHunters.MinerMonitor.RigClient
{
    public class FirebaseLogger : IRemoteLogger
    {
        private readonly TimeSpan WAIT_TIME = TimeSpan.FromSeconds(12);

        private readonly FirebaseClient FirebaseClient;
        private ChildQuery Root => FirebaseClient.Child("Rigs").Child(Environment.MachineName);

        public FirebaseLogger(IConfigProvider configProvider)
        {
            FirebaseClient = new FirebaseClient("https://rigcontrol-23592.firebaseio.com/",
                new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(configProvider.FirebaseKey) });
        }

        public void HealthCheck()
        {
            FirebasePut("HealthCheck", DateTime.Now.ToNice());
        }

        public void ServiceStart()
        {
            FileLogger.LogInfo("Service starts");
            FirebasePut("LastStart", DateTime.Now.ToNice());
            FirebasePost("ServiceStarts", DateTime.Now.ToNice());
        }

        private void FirebasePut<T>(string path, T value)
        {
            try
            {
                Root.Child(path).PutAsync(value).Wait(WAIT_TIME);
            }
            catch (Exception e)
            {
                FileLogger.LogError(e);
                Console.WriteLine(e);
            }
        }

        private void FirebasePost<T>(string path, T value)
        {
            try
            {
                Root.Child(path).PostAsync(value).Wait(WAIT_TIME);
            }
            catch (Exception e)
            {
                FileLogger.LogError(e);
                Console.WriteLine(e);
            }
        }
    }
}
