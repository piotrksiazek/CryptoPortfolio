using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Interfaces
{
    public interface IBalanceRepository : IUserOwnedRepository<Balance>
    {
        Task<Balance> GetForUserWithCryptoId(string appUserId, int cryptoId);
    }
}
