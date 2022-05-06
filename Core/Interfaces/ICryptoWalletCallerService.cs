using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICryptoWalletCallerService
    {
        public Task<List<Transaction>> GetTransactionList(string address, Cryptocurrency crypto, string appUserId);
        public Task<decimal> GetWalletBalance(string address, Cryptocurrency crypto);
    }
}
