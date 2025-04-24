using MediatR;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Domain.Entities;

namespace TaskManagment.Application.Features.Project;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CustomResponse<CreateProjectResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Project> _projectRepository;
    private readonly IGenericRepository<Domain.Entities.Mission> _projectMissionRepository;
    private readonly IGenericRepository<ProjectUser> _projectUserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectCommandHandler(
        IGenericRepository<Domain.Entities.Project> projectRepository,
        IGenericRepository<Domain.Entities.Mission> projectMissionRepository,
        IGenericRepository<ProjectUser> projectUserRepository,
        IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _projectMissionRepository = projectMissionRepository;
        _projectUserRepository = projectUserRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponse<CreateProjectResponse>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var newProject = new Domain.Entities.Project
        {
            Name = request.Name,
            Description = request.Description,
            CustomerId = request.CustomerId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _projectRepository.AddAsync(newProject);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var userId in request.ProjectUserIds)
        {
            var projectUser = new ProjectUser
            {
                ProjectId = newProject.Id,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _projectUserRepository.AddAsync(projectUser);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateProjectResponse
        {
            ProjectId = newProject.Id,
            Name = newProject.Name,
            Description = newProject.Description,
            CustomerId = newProject.CustomerId,
            MissionIds = request.MissionIds,
            ProjectUserIds = request.ProjectUserIds
        };

        return CustomResponse<CreateProjectResponse>.SuccessResponse(response, "Proje başarıyla oluşturuldu.");
    }
}
