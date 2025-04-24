using MediatR;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Domain.Entities;

namespace TaskManagment.Application.Features.Project.Commands;

public class ProjectUserAddCommandHandler  : IRequestHandler<ProjectUserAddCommand, bool>
{
    private readonly IGenericRepository<ProjectUser> _projectUserRepository;
    private readonly IUnitOfWork _unitOfWork;


    public ProjectUserAddCommandHandler(IGenericRepository<ProjectUser> projectUserRepository, IUnitOfWork uow)
    {
        _projectUserRepository = projectUserRepository;
        _unitOfWork = uow;
    }

    public async Task<bool> Handle(ProjectUserAddCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var projectUser = new ProjectUser
            {
                ProjectId = request.PId,
                UserId = request.UId
            };
            await _projectUserRepository.AddAsync(projectUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}