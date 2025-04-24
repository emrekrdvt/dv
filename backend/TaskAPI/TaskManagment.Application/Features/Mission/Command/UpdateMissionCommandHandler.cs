using AutoMapper;
using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Mission.Command;

public class UpdateMissionCommandHandler : IRequestHandler<UpdateMissionCommand, CustomResponse<MissionResponseDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Entities.Project> _projectRepository;
    private readonly IGenericRepository<Domain.Entities.Mission> _missionRepository;
    private readonly IGenericRepository<Domain.Entities.ProjectUser> _procejtUserRepository;

    public UpdateMissionCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IGenericRepository<Domain.Entities.Project> projectRepository,
        IGenericRepository<Domain.Entities.Mission> missionRepository,
        IGenericRepository<Domain.Entities.ProjectUser> procejtUserRepository
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _projectRepository = projectRepository;
        _missionRepository = missionRepository;
        _procejtUserRepository = procejtUserRepository;
    }

    public async Task<CustomResponse<MissionResponseDTO>> Handle(UpdateMissionCommand request, CancellationToken cancellationToken)
    {
        var mission = await _missionRepository.GetByIdAsync(request.Id);
        if (mission == null)
        {
            return CustomResponse<MissionResponseDTO>.FailResponse("Görev bulunamadı.");
        }

        var userProjects = await _procejtUserRepository.FindAsync(
            x => x.ProjectId == request.ProjectId && x.UserId == request.UserId);
        if (userProjects == null)
        {
            return CustomResponse<MissionResponseDTO>.FailResponse("Projeye dahil değilsiniz.");
        }

        mission.Title = request.Title ?? mission.Title; 
        mission.Description = request.Description ?? mission.Description;
        mission.AssignedUserId = request.AssignedUserId != 0 ? request.AssignedUserId : mission.AssignedUserId;
        mission.UpdatedAt = DateTime.UtcNow ;
        mission.ProjectId = request.ProjectId;
        mission.UserId = request.UserId;
        mission.Status = request.Status ?? mission.Status;

        _missionRepository.Update(mission);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = _mapper.Map<MissionResponseDTO>(mission);
        return CustomResponse<MissionResponseDTO>.SuccessResponse(response, "Görev güncellendi.");
    }
}