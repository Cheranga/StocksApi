using System.Net.Http;
using AutoMapper;
using StocksApi.Models;

namespace StocksApi.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Stock, GetStockQuoteResponse>();
            CreateMap<HttpResponseMessage, Stock>().ConvertUsing((new StockQuoteMapper()));
        }
    }
}