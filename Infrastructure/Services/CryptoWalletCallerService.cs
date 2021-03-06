using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Newtonsoft.Json;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class CryptoWalletCallerService : ICryptoWalletCallerService
    {
        private IExternalApiClientService _httpClient;
        private const string _etherScanBaseUrl = "https://api.etherscan.io/api?";

        public CryptoWalletCallerService()
        {
        }

        public CryptoWalletCallerService(IExternalApiClientService httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Transaction>?> GetTransactionList(string address, Cryptocurrency crypto, string appUserId)
        {
            string url;
            if(crypto.Name.ToLower().Equals("ethereum"))
            {
                url = _etherScanBaseUrl + "module=account&action=txlist&address=" + address + "&startblock=0&endblock=99999999&sort=asc&apikey=" + Environment.GetEnvironmentVariable("EtherscanApiKey");
                var json = await _httpClient.GetAsync(url);
                var wallet = JsonConvert.DeserializeObject<EtherscanWallet>(json);
                return EvaluateEthTransactions(wallet, crypto, appUserId);
            }
            return null;
        }

        public async Task<decimal?> GetWalletBalance(string address, Cryptocurrency crypto)
        {
            string url;
            if (crypto.Name.ToLower().Equals("ethereum"))
            {
                url = _etherScanBaseUrl + "module=account&action=balance&address=" + address + "&tag=latest&apikey=" + Environment.GetEnvironmentVariable("EtherscanApiKey");
                var json = await _httpClient.GetAsync(url);
                var ethBalance = JsonConvert.DeserializeObject<EtherscanBalance>(json).Result;
                return ethBalance == null || ethBalance == "" ? null : decimal.Parse(ethBalance);
            }
            return null;
        }

        private List<Transaction> EvaluateEthTransactions(EtherscanWallet wallet, Cryptocurrency crypto, string appUserId)
        {
            List<Transaction> result = new();
            foreach(var transaction in wallet.Result)
            {
                Transaction currentTransaction = new();

                currentTransaction.Cryptocurrency = crypto;
                currentTransaction.CryptocurrencyId = crypto.Id;
                currentTransaction.AppUserId = appUserId;
                currentTransaction.Amount = decimal.Parse(transaction.Value) - decimal.Parse(transaction.Gas);

                result.Add(currentTransaction);
            }
            return result;
        }
    }
}
