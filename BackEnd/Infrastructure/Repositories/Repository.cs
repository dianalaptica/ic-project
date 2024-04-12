using System.Linq.Expressions;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ToursitDbContext _appDbContext;

    public Repository(ToursitDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IQueryable<T> FindAll(bool trackChanges)
    {
        return !trackChanges
            ? _appDbContext.Set<T>()
                .AsNoTracking()
            : _appDbContext.Set<T>();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return !trackChanges
            ? _appDbContext.Set<T>()
                .Where(expression)
                .AsNoTracking()
            : _appDbContext.Set<T>()
                .Where(expression);
    }

    public void Create(T entity)
    {
        _appDbContext.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _appDbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _appDbContext.Set<T>().Remove(entity);
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _appDbContext.SaveChangesAsync();
    }
}