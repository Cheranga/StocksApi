using System;
using AutoMapper;
using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StocksApi;
using StocksApi.Mappers;
using StocksApi.Services;

[assembly: FunctionsStartup(typeof(Startup))]

namespace StocksApi
{
    public class Startup : FunctionsStartup
    {
        protected virtual IConfigurationRoot GetConfigurationRoot(IFunctionsHostBuilder functionsHostBuilder)
        {
            var services = functionsHostBuilder.Services;

            var executionContextOptions = services
                .BuildServiceProvider()
                .GetService<IOptions<ExecutionContextOptions>>()
                .Value;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(executionContextOptions.AppDirectory)
                .AddJsonFile("local.settings.json", true)
                .AddEnvironmentVariables()
                .Build();

            return configuration;
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;
            RegisterHttpClients(services);
            RegisterValidators(services);
            RegisterMappings(services);
        }

        private void RegisterHttpClients(IServiceCollection services)
        {
            services.AddHttpClient<IStockQuoteService, StockQuoteService>(client =>
            {
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("StockApi:Url"));
                client.DefaultRequestHeaders.Add("X-Finnhub-Token", Environment.GetEnvironmentVariable("StockApi:ApiKey"));
            });
        }

        private void RegisterMappings(IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
        }

        private void RegisterValidators(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
        }
    }
}