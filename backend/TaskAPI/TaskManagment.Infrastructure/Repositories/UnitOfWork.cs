using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Infrastructure.Persistence;

namespace TaskManagment.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (_context == null)
        {
            throw new InvalidOperationException("DbContext is not initialized.");
        }
        return await _context.SaveChangesAsync(cancellationToken);
    }
}