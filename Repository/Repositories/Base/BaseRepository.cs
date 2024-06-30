using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Contexts;

namespace Repository.Repositories.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly AppDbContext _appDbContext;

    public BaseRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>>? queryFunc = null)
    {
        IQueryable<T> query = _appDbContext.Set<T>().AsNoTracking();

        if (queryFunc != null)
            query = queryFunc(query);

        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }

    public async Task<List<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? queryFunc = null)
    {
        IQueryable<T> query = _appDbContext.Set<T>().AsNoTracking();

        if (queryFunc != null)
            query = queryFunc(query);

        return await query.ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        _appDbContext.Set<T>().Add(entity);
        await _appDbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<List<T>> AddListAsync(List<T> entity)
    {
        _appDbContext.Set<T>().AddRange(entity);
        await _appDbContext.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _appDbContext.Set<T>().Update(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _appDbContext.Set<T>().Remove(entity);
        await _appDbContext.SaveChangesAsync();
    }
}