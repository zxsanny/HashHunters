using Autofac;
using HashHunters.MinerMonitor.RigClient;
using NUnit.Framework;

namespace HashHunters.Test
{
    [TestFixture]
    public class LoggerTest : BaseTest
    {
        [Test]
        public void HealthCheckTest()
        {
            Container.Resolve<IRemoteLogger>().HealthCheck();
        }
    }
}
