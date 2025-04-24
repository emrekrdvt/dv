using MediatR;
using TaskManagment.Application.Features.User;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Admin;

public class UpdateUserCommand : IRequest<CustomResponse<UserQueryDTO>>
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool Admin { get; set; }
    public bool? isDelMethod { get; set; } = false;
}