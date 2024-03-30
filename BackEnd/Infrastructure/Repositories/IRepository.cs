using System.Linq.Expressions;

namespace BackEnd.Infrastructure.Repositories;

public interface IRepository<T>
{
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}