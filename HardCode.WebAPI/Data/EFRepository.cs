using HardCode.WebAPI.Entities;
using HardCode.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HardCode.WebAPI.Data
{
    public class EFRepository<T> : IAsyncRepository<T> where T : class, IEntity<int>
    {
        protected readonly HardCodeContext _dbContext;

        public EFRepository(HardCodeContext flowContext)
        {
            _dbContext = flowContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
