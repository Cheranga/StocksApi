using Newtonsoft.Json;

namespace StocksApi.Models
{
    public class Stock
    {
        [JsonProperty("o")]
        public decimal OpenPrice { get; set; }

        [JsonProperty("h")]
        public decimal HighPrice { get; set; }

        [JsonProperty("l")]
        public decimal LowPrice { get; set; }

        [JsonProperty("c")]
        public decimal CurrentPrice { get; set; }

        [JsonProperty("pc")]
        public decimal PreviousClosingPrice { get; set; }
    }
}