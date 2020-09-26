namespace StocksApi.Models
{
    public class GetStockQuoteRequest
    {
        public string Symbol { get; }

        public GetStockQuoteRequest(string symbol)
        {
            Symbol = symbol;
        }
    }
}