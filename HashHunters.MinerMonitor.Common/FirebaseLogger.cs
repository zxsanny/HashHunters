using System;
using Firebase.Database;
using HashHunters.MinerMonitor.Common.Interfaces;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace HashHunters.MinerMonitor.Common
{
    public class FirebaseLogger : ILogger
    {
        private readonly IConfigProvider ConfigProvider;

        public FirebaseLogger(IConfigProvider configProvider)
        {
            ConfigProvider = configProvider;
        }

        public void HealthCheck()
        {
            var firebaseClient = new FirebaseClient("https://rigcontrol-23592.firebaseio.com/",
                new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(ConfigProvider.FirebaseKey) });

            var lastHealthField = firebaseClient.Child("Rigs").Child(Environment.MachineName).Child("HealthCheck");
            lastHealthField.PutAsync(DateTime.Now).Wait();
        }

        public void ServiceStart()
        {
            throw new System.NotImplementedException();
        }
    }
}
