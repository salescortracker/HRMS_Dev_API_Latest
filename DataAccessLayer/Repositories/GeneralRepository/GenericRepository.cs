using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories.GeneralRepository
{
    public class GenericRepository<T> : IGeneralRepository<T> where T : class
    {
        private readonly HRMSContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(HRMSContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        
        public IQueryable<T> Query() => _dbSet.AsQueryable();


        public async Task<IEnumerable<T>> BulkCreateAsync(IEnumerable<T> entities)
        {
            try
            {
                await _dbSet.AddRangeAsync(entities);
                Log.Information("Bulk created {Count} {Entity} records", entities.Count(), typeof(T).Name);
                return entities;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error bulk creating {Entity} records", typeof(T).Name);
                throw;
            }
        }

        public async Task<bool> SoftDeleteAsync(object id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null) return false;

                var isActiveProp = typeof(T).GetProperty("IsActive");
                if (isActiveProp == null || !isActiveProp.CanWrite)
                {
                    Log.Warning("{Entity} does not have a writable IsActive property", typeof(T).Name);
                    return false;
                }

                isActiveProp.SetValue(entity, false);
                _dbSet.Update(entity);
                

                Log.Information("Soft-deleted {Entity} with Id {Id} by setting IsActive = false", typeof(T).Name, id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error soft-deleting {Entity} with Id {Id}", typeof(T).Name, id);
                throw;
            }

        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {

                _dbSet.Update(entity);
                Log.Information("Updated {Entity}", typeof(T).Name);
                return entity;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating {Entity}", typeof(T).Name);
                throw;
            }
        }


    }
}
