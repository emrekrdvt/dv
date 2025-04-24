using MediatR;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Domain.Entities;

namespace TaskManagment.Application.Features.Admin;

public class CreateHistoryCommand : IRequest<CustomResponse<History>>
{
    public int UserId { get; set; }
    public int MissionId { get; set; }
    public string Action { get; set; } = null!;
    public int ProjectId { get; set; }
}