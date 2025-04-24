using System.Linq.Expressions;
using TaskManagment.Domain.Entities;


namespace TaskManagment.Application.Interfaces.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> FindAsyncAll(Expression<Func<T, bool>> predicate);
    Task<T?> FindWithIncludeAsync(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>>[]? includes = null,
        string[]? includeStrings = null);
    
    
    Task<List<T>> ListWithIncludeAsync(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>>[]? includes = null,
        string[]? includeStrings = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? skip = null,
        int? take = null,
        bool asNoTracking = false);

}