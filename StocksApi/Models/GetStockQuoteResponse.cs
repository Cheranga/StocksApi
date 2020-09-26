namespace StocksApi.Models
{
    public class GetStockQuoteResponse
    {
        public decimal OpenPrice { get; set; }

        public decimal HighPrice { get; set; }

        public decimal LowPrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal PreviousClosingPrice { get; set; }
    }
}