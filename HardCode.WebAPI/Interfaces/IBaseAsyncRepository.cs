using HardCode.WebAPI.Entities;

namespace HardCode.WebAPI.Interfaces
{
    public interface IBaseAsyncRepository<T, TKey>
        where T : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<T> GetByIdAsync(TKey id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> Query();
    }
}
