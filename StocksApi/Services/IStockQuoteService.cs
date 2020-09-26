using System.Threading.Tasks;
using StocksApi.Core;
using StocksApi.Models;

namespace StocksApi.Services
{
    public interface IStockQuoteService
    {
        Task<Result<GetStockQuoteResponse>> GetQuoteAsync(GetStockQuoteRequest request);
    }
}