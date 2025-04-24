using MediatR;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Domain.Entities;

namespace TaskManagment.Application.Features.Project.Commands;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, CustomResponse<CreateProjectResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Project> _projectRepository;
    private readonly IGenericRepository<Domain.Entities.Mission> _projectMissionRepository;
    private readonly IGenericRepository<ProjectUser> _projectUserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectCommandHandler(
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
    public async Task<CustomResponse<CreateProjectResponse>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var checkProject = await _projectRepository.FindAsync(x => x.Id == request.ProjectId);
        if (checkProject == null)
            return CustomResponse<CreateProjectResponse>.FailResponse("Proje bulunamadi.");
        
        checkProject.Name = request.Name ?? checkProject.Name;
        checkProject.Description = request.Description ?? checkProject.Description;
        checkProject.CustomerId = request.CustomerId != 0 ? request.CustomerId : checkProject.CustomerId;
        checkProject.UpdatedAt = DateTime.UtcNow;
  
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        var response = new CreateProjectResponse
        {
            ProjectId = checkProject.Id,
            Name = checkProject.Name,
            Description = checkProject.Description,
            CustomerId = checkProject.CustomerId
        };
        return CustomResponse<CreateProjectResponse>.SuccessResponse(response, "Basariliyla guncellendi");
        
    }
}