using Core.Entities;
using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CryptoApiCallerService : ICryptoApiCallerService
    {
        private IExternalApiClientService _httpClient;
        private readonly string _coinGeckoBaseUri = "https://api.coingecko.com/api/v3/";

        public CryptoApiCallerService(IExternalApiClientService httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CryptocurrencyData?> GetCryptoData(string name)
        {
            string url = _coinGeckoBaseUri + "coins" + "/" + name;

            string? json = await _httpClient.Get(url);
            if (json != null)
            {
                return JsonSerializer.Deserialize<CryptocurrencyData>(json);
            }
            return null;
        }

        public async Task<double> GetCryptoPrice(string name)
        {
            var result = await GetCryptoData(name);

            return result.market_data.current_price.usd;
        }
    }
}
