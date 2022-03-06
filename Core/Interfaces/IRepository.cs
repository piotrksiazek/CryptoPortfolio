using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : IOwnedByUser
    {
        Task<T> Get(int id);
        Task<T?> GetForUser(string appUserId, int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllForUser(string appUserId);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
