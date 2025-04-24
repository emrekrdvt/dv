using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Admin;

public class CreateUserCommand : IRequest<CustomResponse<int>>
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool Admin { get; set; }
}