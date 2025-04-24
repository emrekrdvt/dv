using System.Linq.Expressions;
using MediatR;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Domain.Entities;

namespace TaskManagment.Application.Features.Admin.Query;

public class GetProjectHistoryQueryHandler : IRequestHandler<GetProjectHistoryQuery, CustomResponse<List<HistoryQueryDto>>>
{
    private readonly IGenericRepository<History> _historyRepository;

    public GetProjectHistoryQueryHandler(IGenericRepository<History> historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public async Task<CustomResponse<List<HistoryQueryDto>>> Handle(GetProjectHistoryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var historyWithIncludes = await _historyRepository.ListWithIncludeAsync(
                predicate: x => x.ProjectId == request.ProjectId,
                includes: new Expression<Func<History, object>>[]
                {
                    x => x.User,
                    x => x.Mission
                }
            );

            var result = historyWithIncludes.Select(h => new HistoryQueryDto
            {
                Action = h.Action,
                Timestamp = h.Timestamp,
                Username = h.User.Username,
                MissionTitle = h.Mission.Title
            }).ToList();

            return result.Count == 0
                ? CustomResponse<List<HistoryQueryDto>>.FailResponse("History yok")
                : CustomResponse<List<HistoryQueryDto>>.SuccessResponse(result, "Başarılı");
        }
        catch (Exception e)
        {
            return CustomResponse<List<HistoryQueryDto>>.FailResponse("Hata: " + e.Message);
        }
    }
}