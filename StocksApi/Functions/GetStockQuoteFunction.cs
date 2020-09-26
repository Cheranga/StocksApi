using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using StocksApi.Models;
using StocksApi.Services;

namespace StocksApi.Functions
{
    public class GetStockQuoteFunction
    {
        private readonly ILogger<GetStockQuoteFunction> _logger;
        private readonly IStockQuoteService _stockQuoteService;

        public GetStockQuoteFunction(IStockQuoteService stockQuoteService, ILogger<GetStockQuoteFunction> logger)
        {
            _stockQuoteService = stockQuoteService;
            _logger = logger;
        }

        [FunctionName(nameof(GetStockQuoteFunction))]
        public async Task<IActionResult> GetTopStocksAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "stocks/{symbol}")]
            HttpRequest request, string symbol)
        {
            var getTopStocksResponse = await _stockQuoteService.GetQuoteAsync(new GetStockQuoteRequest(symbol));

            if (getTopStocksResponse.Status)
            {
                return new OkObjectResult(getTopStocksResponse.Data);
            }

            _logger.LogError("Error when getting quote for: {symbol}", symbol);
            return new InternalServerErrorResult();
        }
    }
}