using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Project;

public class UpdateProjectCommand : IRequest<CustomResponse<CreateProjectResponse>>
{
    public int ProjectId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public int CustomerId { get; set; }
}