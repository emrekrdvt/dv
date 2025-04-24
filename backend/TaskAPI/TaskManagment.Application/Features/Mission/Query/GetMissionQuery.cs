using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Mission.Query;

public class GetMissionQuery : IRequest<CustomResponse<List<GetMissionQueryDto>>>
{
    public int pid { get; set; }
    public int userid { get; set; }
}