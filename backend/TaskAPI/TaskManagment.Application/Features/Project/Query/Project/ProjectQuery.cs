using MediatR;

namespace TaskManagment.Application.Features.User;

public class ProjectQuery : IRequest<List<ProjectResponse>>
{
    public int UserId { get; set; }
    public int? ProjectId { get; set; } 
}