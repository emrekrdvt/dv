using MediatR;

namespace TaskManagment.Application.Features.Project.Commands;

public class ProjectUserAddCommand : IRequest<bool>
{
    public int PId { get; set; }
    public int UId { get; set; }
}