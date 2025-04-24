using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Project;

public class CreateProjectCommand : IRequest<CustomResponse<CreateProjectResponse>>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public List<int> MissionIds { get; set; } = new();
    public List<int> ProjectUserIds { get; set; } = new();
}