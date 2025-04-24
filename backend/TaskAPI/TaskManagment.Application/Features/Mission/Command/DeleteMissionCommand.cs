using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Mission.Command;

public class DeleteMissionCommand : IRequest<CustomResponse<bool>>
{
    public int MissionId { get; set; }
    public int UserId { get; set; }
}