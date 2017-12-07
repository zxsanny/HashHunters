using Autofac;
using HashHunters.MinerMonitor.Common.Interfaces;
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
