using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserOwnedRepository<T> : IRepository<T> where T : IOwnedByUser
    {
        Task<T?> GetForUser(string appUserId, int id);
        Task<IEnumerable<T>> GetAllForUser(string appUserId);
    }
}
