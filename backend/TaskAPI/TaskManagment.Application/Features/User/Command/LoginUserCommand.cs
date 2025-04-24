using MediatR;

namespace TaskManagment.Application.Features.User.Command;

public class LoginUserCommand : IRequest<LoginUserResponse>
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}