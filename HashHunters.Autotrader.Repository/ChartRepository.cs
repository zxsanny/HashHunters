using HashHunters.Autotrader.Core.DTO;
using HashHunters.Autotrader.Core.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashHunters.Autotrader.Repository
{
    public class ChartRepository : IChartRepository
    {
        readonly IMongoDatabase MongoDatabase;

        public ChartRepository(IMongoDatabase mongoDatabase)
        {
            MongoDatabase = mongoDatabase;
        }

        public Task SaveChartAsync(CurrencyPair currencyPair, CandleInterval interval, List<CandleData> data)
        {
            var collection = MongoDatabase.GetCollection<CandleData>($"chart_{currencyPair}_{interval}");
            return collection.InsertManyAsync(data);
        }
    }
}
