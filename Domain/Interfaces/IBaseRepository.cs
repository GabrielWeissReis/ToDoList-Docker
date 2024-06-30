namespace Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>>? queryFunc = null);
    Task<List<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? queryFunc = null);
    Task<T> AddAsync(T entity);
    Task<List<T>> AddListAsync(List<T> entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
