using Autofac;
using HashHunters.Autotrader.Core.Interfaces;

namespace HashHunters.Autotrader.Services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SecurityService>().As<ISecurityService>();
            builder.RegisterType<CryptoProvider>().As<IHHCryptoProvider>();
            builder.RegisterType<BittrexBroker>().As<IMarketBroker>();

            base.Load(builder);
        }
    }
}
