using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CryptoWalletCallerService : ICryptoWalletCallerService
    {
        private IExternalApiClientService _httpClient;
        public CryptoWalletCallerService(IExternalApiClientService httpClient)
        {
            _httpClient = httpClient;
        }
        public WalletData? GetWalletData(string address, string cryptocurrencyName)
        {
            throw new NotImplementedException();
        }
    }
}
