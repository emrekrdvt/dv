using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Domain.Entities;
using TaskManagment.Infrastructure.Persistence;

namespace TaskManagment.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public void Update(T entity) => _dbSet.Update(entity);
    
    public void Delete(T entity) => _dbSet.Remove(entity);
    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate) => await _dbSet.FirstOrDefaultAsync(predicate);

    public Task<List<T>> FindAsyncAll(Expression<Func<T, bool>> predicate)
    {
        var a =  _dbSet.Where(predicate).ToListAsync();
        return a;
    }

    public async Task<T?> FindWithIncludeAsync(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>>[]? includes = null,
        string[]? includeStrings = null)
    {
        IQueryable<T> query = _dbSet;

        if (includes != null)
        {
            foreach (var include in includes)
                query = query.Include(include);
        }

        if (includeStrings != null)
        {
            foreach (var includeString in includeStrings)
                query = query.Include(includeString);
        }

        return await query.FirstOrDefaultAsync(predicate);
    }
    
    public async Task<List<T>> ListWithIncludeAsync(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>>[]? includes = null,
        string[]? includeStrings = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? skip = null,
        int? take = null,
        bool asNoTracking = false)
    {
        IQueryable<T> query = _dbSet;
        if (includes != null)
        {
            foreach (var include in includes)
                query = query.Include(include);
        }

        if (includeStrings != null)
        {
            foreach (var includeString in includeStrings)
                query = query.Include(includeString);
        }
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        query = query.Where(predicate);

        if (orderBy != null)
        {
            query = orderBy(query);
        }
        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }
        return await query.ToListAsync();
    }

}