using System;
using Firebase.Database;
using HashHunters.MinerMonitor.Common.Interfaces;
using System.Threading.Tasks;
using Firebase.Database.Query;
using HashHunters.MinerMonitor.Common.Extensions;

namespace HashHunters.MinerMonitor.Common
{
    public class FirebaseLogger : ILogger
    {
        private readonly FirebaseClient FirebaseClient;
        private ChildQuery Root => FirebaseClient.Child("Rigs").Child(Environment.MachineName);

        public FirebaseLogger(IConfigProvider configProvider)
        {
            FirebaseClient = new FirebaseClient("https://rigcontrol-23592.firebaseio.com/",
                new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(configProvider.FirebaseKey) });
        }

        public void HealthCheck()
        {
            Root.Child("HealthCheck").PutAsync<string>(DateTime.Now.ToNice()).Wait();
        }

        public void ServiceStart()
        {
            Root.Child("LastStart").PutAsync<string>(DateTime.Now.ToNice()).Wait();
            Root.Child("ServiceStarts").PostAsync<string>(DateTime.Now.ToNice()).Wait();
        }
    }
}
