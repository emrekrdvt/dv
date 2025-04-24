using System.Linq.Expressions;
using System.Runtime.InteropServices.ObjectiveC;
using MediatR;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Domain.Enums;

namespace TaskManagment.Application.Features.Mission.Query;

public class GetMyMissionQueryHandler : IRequestHandler<GetMyMissionQuery, CustomResponse<List<MyMissionDto>>>
{
    private readonly IGenericRepository<Domain.Entities.Mission> _missionRepository;

    public GetMyMissionQueryHandler(IGenericRepository<Domain.Entities.Mission> missionRepository)
    {
        _missionRepository = missionRepository;
    }
public async Task<CustomResponse<List<MyMissionDto>>> Handle(GetMyMissionQuery request,
    CancellationToken cancellationToken)
{
    try
    {
        var today = DateTime.Now.Date;
        var lstMission = new List<Domain.Entities.Mission>();

        if (request.today)
        {
            lstMission = await _missionRepository.ListWithIncludeAsync(
                predicate: p => p.AssignedUserId == request.UserId &&
                                p.IsDeleted == false &&
                                p.CreatedAt.Date == today,
                includes: new Expression<Func<Domain.Entities.Mission, object>>[]
                {
                    x => x.Project,
                    x => x.Project.Customer
                },
                includeStrings: null,
                orderBy: q => q.OrderByDescending(p => p.CreatedAt),
                skip: 0,
                take: 10,
                asNoTracking: true
            );
        }
        else
        {
            lstMission = await _missionRepository.ListWithIncludeAsync(
                predicate: p => p.AssignedUserId == request.UserId && p.IsDeleted == false,
                includes: new Expression<Func<Domain.Entities.Mission, object>>[]
                {
                    x => x.Project,
                    x => x.Project.Customer
                },
                includeStrings: null,
                orderBy: q => q.OrderByDescending(p => p.CreatedAt),
                skip: 0,
                take: 10,
                asNoTracking: true
            );
        }

        var result = lstMission.Select(x => new MyMissionDto
        {
            Id = x.Id,
            Name = x.Title ?? string.Empty,
            Description = x.Description,
            Status = new StatusObjMymisson
            {
                Status = x.Status,
                StatusName = x.Status.ToString()
            },
            MyMissionProject = x.Project != null
                ? new MyMissionProject
                {
                    Id = x.Project.Id,
                    createdat = x.CreatedAt,
                    Name = x.Project.Name ?? string.Empty,
                    Description = x.Project.Description,
                    CustomerId = x.Project.CustomerId,
                    Customer = x.Project.Customer != null
                        ? new MyCustomer
                        {
                            Name = x.Project.Customer.Name ?? string.Empty
                        }
                        : null
                }
                : null
        }).ToList();

        return CustomResponse<List<MyMissionDto>>.SuccessResponse(result, "Ok");
    }
    catch (Exception ex)
    {
        return CustomResponse<List<MyMissionDto>>.FailResponse(ex.Message);
    }
}
}