using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MP.SimpleTokens.Common.Ethereum.Interfaces;
using MP.SimpleTokens.Common.Ethereum.Models;
using Nethereum.Web3;

namespace MP.SimpleTokens.Common.Ethereum
{
    public static class Dependencies
    {
        public static IServiceCollection AddEthereum(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection($"Ethereum");

            services.Configure<EthereumConfiguration>(configSection);
            services.AddSingleton(w => new Web3(configSection.Get<EthereumConfiguration>().Web3Url));
            services.AddTransient<IEthereumService, EthereumService>();

            return services;
        }
    }
}