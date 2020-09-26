using System.Net.Http;
using AutoMapper;
using Newtonsoft.Json;
using StocksApi.Models;

namespace StocksApi.Mappers
{
    public class StockQuoteMapper : ITypeConverter<HttpResponseMessage, Stock>
    {
        public Stock Convert(HttpResponseMessage source, Stock destination, ResolutionContext context)
        {
            if (source == null || !source.IsSuccessStatusCode)
            {
                return null;
            }

            var content = source.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }

            var stockQuote = JsonConvert.DeserializeObject<Stock>(content, new JsonSerializerSettings
                { Error = (sender, args) => args.ErrorContext.Handled = true });

            return stockQuote;
        }
    }
}