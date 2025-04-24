using AutoMapper;
using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Mission.Command;

public class DeleteMissionCommandHandler : IRequestHandler<DeleteMissionCommand, CustomResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Entities.Mission> _missionRepository;
    private readonly IGenericRepository<Domain.Entities.ProjectUser> _projectUserRepository;

    public DeleteMissionCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IGenericRepository<Domain.Entities.Mission> missionRepository,
        IGenericRepository<Domain.Entities.ProjectUser> projectUserRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _missionRepository = missionRepository;
        _projectUserRepository = projectUserRepository;
    }

    public async Task<CustomResponse<bool>> Handle(DeleteMissionCommand request, CancellationToken cancellationToken)
    {
        var mission = await _missionRepository.GetByIdAsync(request.MissionId);
        if (mission == null || mission.IsDeleted)
        {
            return CustomResponse<bool>.FailResponse("Görev bulunamadı.");
        }

        var userProject = await _projectUserRepository.FindAsync(
            x => x.ProjectId == mission.ProjectId && x.UserId == request.UserId);
        if (userProject == null)
        {
            return CustomResponse<bool>.FailResponse("Bu projeye erişiminiz yok.");
        }

        mission.IsDeleted = true;
        _missionRepository.Update(mission);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CustomResponse<bool>.SuccessResponse(true, "Görev başarıyla silindi.");
    }
}