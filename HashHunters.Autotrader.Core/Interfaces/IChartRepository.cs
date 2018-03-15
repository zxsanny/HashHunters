using HashHunters.Autotrader.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HashHunters.Autotrader.Core.Interfaces
{
    public interface IChartRepository
    {
        Task SaveChartAsync(CurrencyPair currencyPair, CandleInterval interval, List<CandleData> data);
    }
}
