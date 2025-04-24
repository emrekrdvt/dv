using AutoMapper;
using MediatR;
using TaskManagment.Application.Interfaces.Persistence;


namespace TaskManagment.Application.Features.Mission.Command;

public class CreateMissionCommandHandler : IRequestHandler<CreateMissionCommand, CustomResponse<MissionResponseDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Entities.Project> _projectRepository;
    private readonly IGenericRepository<Domain.Entities.Mission> _missionRepository;

    public CreateMissionCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IGenericRepository<Domain.Entities.Project> projectRepository,
        IGenericRepository<Domain.Entities.Mission> missionRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _projectRepository = projectRepository;
        _missionRepository = missionRepository;
    }

    public async Task<CustomResponse<MissionResponseDTO>> Handle(CreateMissionCommand request, CancellationToken cancellationToken)
    {
        var userProjects = await _projectRepository.FindAsync(x => x.ProjectUsers.Any(pu => pu.UserId == request.UserId));
        if (userProjects == null)
        {
            return CustomResponse<MissionResponseDTO>.FailResponse("Projeye dahil değilsiniz.");
        }
        var checkProject = await _projectRepository.FindAsync(x=> x.Id == request.ProjectId);
        if (checkProject == null)
        {
            return CustomResponse<MissionResponseDTO>.FailResponse("Proje bulunamadı.");
        }

        var createMission = new Domain.Entities.Mission
        {
            Title = request.Title,
            Description = request.Description,
            UserId = request.UserId,
            ProjectId = request.ProjectId,
            AssignedUserId = request.AssignedUserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _missionRepository.AddAsync(createMission);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = _mapper.Map<MissionResponseDTO>(createMission);
        return CustomResponse<MissionResponseDTO>.SuccessResponse(response, "Görev başarıyla oluşturuldu.");
    }
}
