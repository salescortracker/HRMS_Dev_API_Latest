using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories.GeneralRepository
{
    public interface IGeneralRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        Task<T> UpdateAsync(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<bool> SoftDeleteAsync(object id);

        IQueryable<T> Query();

        // Bulk operations
        Task<IEnumerable<T>> BulkCreateAsync(IEnumerable<T> entities);
    }
}
