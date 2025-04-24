using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Mission.Command;

public class CreateMissionCommand : IRequest<CustomResponse<MissionResponseDTO>>
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int ProjectId { get; set; }
    public int? AssignedUserId { get; set; }
}