using MediatR;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Domain.Enums;

namespace TaskManagment.Application.Features.Mission.Command;

public class UpdateMissionCommand : IRequest<CustomResponse<MissionResponseDTO>>
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = string.Empty;

    public int UserId { get; set; }

    public MissionStatus? Status { get; set; }
    
    public int ProjectId { get; set; }

    public int? AssignedUserId { get; set; }
}