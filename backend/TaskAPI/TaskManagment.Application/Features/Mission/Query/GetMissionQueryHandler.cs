using System.Linq.Expressions;
using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Mission.Query;

public class GetMissionQueryHandler : IRequestHandler<GetMissionQuery, CustomResponse<List<GetMissionQueryDto>>>
{
    
    private readonly IGenericRepository<Domain.Entities.Mission> _missionRepository;
    
    public GetMissionQueryHandler(IGenericRepository<Domain.Entities.Mission> missionRepository)
    {
        _missionRepository = missionRepository;
    }

    public async Task<CustomResponse<List<GetMissionQueryDto>>> Handle(GetMissionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            
            var a = await _missionRepository.ListWithIncludeAsync(
                predicate: p => p.ProjectId == request.pid && p.IsDeleted==false,
                includes: new Expression<Func<Domain.Entities.Mission, object>>[]
                {
                    x => x.AssignedUser,
                    x => x.Project
                },
                includeStrings: new[] { "AssignedUser" },
                orderBy: q => q.OrderByDescending(p => p.CreatedAt),
                skip: 0,
                take: 10,
                asNoTracking: true
            );
           
            if (a == null)
                return CustomResponse<List<GetMissionQueryDto>>.FailResponse("Bulunamadi");
            var result = a.Select(x => new GetMissionQueryDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
                Status = new StatusObj
                {
                    Status = x.Status,
                    StatusName = x.Status.ToString()
                },
                AssignedUser = new ProjectUserDto
                {
                    UserId = x.AssignedUser.Id,
                    Username = x.AssignedUser.Username
                }
            }).ToList();
            return CustomResponse<List<GetMissionQueryDto>>.SuccessResponse(result, "ok");
        }
        catch (Exception e)
        {
            return CustomResponse<List<GetMissionQueryDto>>.FailResponse("Hata");
        }
    }
}