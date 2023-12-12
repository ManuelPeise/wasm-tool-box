using Data.Models.Entities;
using System.Linq.Expressions;

namespace Data.Shared.Interfaces
{
    public interface IRepository<T> where T : AEntity
    {
        Task<IList<T>> GetAllAsync(
          Expression<Func<T, bool>>? expression = null, bool asNoTracking = true);
        Task<T?> Get(Expression<Func<T, bool>> expression, bool asNoTracking = true);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true);
        Task<KeyValuePair<int, T?>> Insert(T entity);
        Task Delete(int id);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}
