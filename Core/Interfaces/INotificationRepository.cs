using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface INotificationRepository : IUserOwnedRepository<Notification>
    {
        /// <summary>
        /// </summary>
        /// <returns>list of distinct cryptocurrency names in notification table</returns>
        Task<List<string>> GetDistinctCryptocurrencyNames();
    }
}
