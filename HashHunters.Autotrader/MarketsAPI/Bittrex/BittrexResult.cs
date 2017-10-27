namespace HashHunters.Autotrader.MarketsAPI.Bittrex
{
    public class BittrexResult<T>
    {
        public string Success { get; set; }
        public string Message { get;set;}
        public T Result { get; set; }
    }
}