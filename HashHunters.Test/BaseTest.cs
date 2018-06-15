using Autofac;
using HashHunters.MinerMonitor.RigClient;
using NUnit.Framework;

namespace HashHunters.Test
{
    [TestFixture]
    public class BaseTest
    {
        protected IContainer Container { get; set; }

        [SetUp]
        public void Setup()
        {
            Container = AutofacConfig.Configure(new ClientModule());
        }
    }
}
