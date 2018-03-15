using Autofac;
using HashHunters.Autotrader.Core.Interfaces;
using MongoDB.Driver;

namespace HashHunters.Autotrader.Repository
{
    public class RepositoryModule : Module
    {
        readonly string ConnectionString;

        public RepositoryModule(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var urlBuilder = new MongoUrlBuilder(ConnectionString);
            builder.Register(ctx =>
                new MongoClient(ConnectionString)
                .GetDatabase(urlBuilder.DatabaseName)
            ).As<IMongoDatabase>().SingleInstance();

            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<ChartRepository>().As<IChartRepository>();

            base.Load(builder);
        }
    }
}
