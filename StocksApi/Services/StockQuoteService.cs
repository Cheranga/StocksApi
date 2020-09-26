using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using StocksApi.Core;
using StocksApi.Models;

namespace StocksApi.Services
{
    public class StockQuoteService : IStockQuoteService
    {
        private readonly HttpClient _client;
        private readonly ILogger<StockQuoteService> _logger;
        private readonly IMapper _mapper;

        public StockQuoteService(HttpClient client, IMapper mapper, ILogger<StockQuoteService> logger)
        {
            _client = client;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<GetStockQuoteResponse>> GetQuoteAsync(GetStockQuoteRequest request)
        {
            var uri = new Uri($"{_client.BaseAddress}/quote?symbol={request.Symbol}");

            var httpResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri));

            if (!httpResponse.IsSuccessStatusCode)
            {
                _logger.LogError("Error when getting quote: {symbol}", request.Symbol);
                return Result<GetStockQuoteResponse>.Failure("symbol", "Cannot retrieve stock quote");
            }

            var stock = _mapper.Map<HttpResponseMessage, Stock>(httpResponse);
            if (stock == null)
            {
                _logger.LogError("Symbol not found: {symbol}", request.Symbol);
                return Result<GetStockQuoteResponse>.Failure("symbol", "Symbol not found");
            }

            var response = _mapper.Map<Stock, GetStockQuoteResponse>(stock);
            return Result<GetStockQuoteResponse>.Success(response);
        }
    }
}