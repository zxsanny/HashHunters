using Binance.Net;
using Bittrex.Net;
using Bittrex.Net.Objects;
using HashHunters.Autotrader.Core.DTO;
using HashHunters.Autotrader.Core.Interfaces;
using HashHunters.Autotrader.Entities;
using HashHunters.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HashHunters.Autotrader.Services
{
    public class BittrexBroker : IMarketBroker
    {
        ISecurityService SecurityService;
        IChartRepository ChartRepository;

        BittrexClient BittrexClient;
        
        public BittrexBroker(ISecurityService securityService, IChartRepository chartRepository)
        {
            SecurityService = securityService;
            ChartRepository = chartRepository;
        }

        public async Task<List<CandleData>> Get(CurrencyPair currencyPair, CandleInterval candleInterval, DateTime from)
        {
            var res = await BittrexClient.GetCandlesAsync(currencyPair.ToBittrex(), candleInterval.ToBittrex());
            return res.Result.Select(c => c.FromBittrex()).ToList();
        }

        public void RunPollingMarketData()
        {
            var exchangeKey = SecurityService.GetKey(ExchangeEnum.Bittrex);
            BittrexClient = new BittrexClient(exchangeKey.ApiKey, exchangeKey.ApiSecret);

            //foreach (var market in CurrencyPairConstants.MainCurrencyPairs)
            //{
            //    foreach (var interval in Enum<CandleInterval>.GetValues())
            //    {
            //        var task = Get(market, interval, DateTime.Now.AddMonths(-1));
            //        task.Wait();
            //        var res = task.Result;
            //    }
            //}
        }
    }

    public static class BittrexConverter
    {
        static Dictionary<CandleInterval, TickInterval> CandleDict = new Dictionary<CandleInterval, TickInterval>
        {
            { CandleInterval.M1, TickInterval.OneMinute },
            { CandleInterval.M5, TickInterval.FiveMinutes },
            { CandleInterval.M30, TickInterval.HalfHour },
            { CandleInterval.M60, TickInterval.OneHour },
            { CandleInterval.M1440, TickInterval.OneDay }
        };

        public static TickInterval ToBittrex(this CandleInterval interval) => CandleDict[interval];

        public static string ToBittrex(this CurrencyPair market)
        {
            var strs = market.ToString().Split('_');
            return $"{strs[1]}-{strs[0]}";
        }

        public static CandleData FromBittrex(this BittrexCandle bittrexCandle) 
            => new CandleData
            {
                Open = bittrexCandle.Open,
                High = bittrexCandle.High,
                Low = bittrexCandle.Low,
                Close = bittrexCandle.Close,
                Volume = bittrexCandle.Volume,
                Timestamp = bittrexCandle.Timestamp,
                BaseVolume = bittrexCandle.BaseVolume
            };
    }
}
