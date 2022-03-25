using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ICryptoApiCallerService
    {
        Task<CryptocurrencyData?> GetCryptoData(string name);
        Task<double?> GetCryptoPrice(string name);
    }
}
