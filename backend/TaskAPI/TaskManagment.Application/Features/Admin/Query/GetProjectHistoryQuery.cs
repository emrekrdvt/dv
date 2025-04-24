using MediatR;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Domain.Entities;

namespace TaskManagment.Application.Features.Admin.Query;

public class GetProjectHistoryQuery : IRequest<CustomResponse<List<HistoryQueryDto>>>
{
    public int ProjectId { get; set; }
}