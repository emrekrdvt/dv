namespace TaskManagment.Application.Interfaces.Persistence;

public interface IUnitOfWork
{
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}