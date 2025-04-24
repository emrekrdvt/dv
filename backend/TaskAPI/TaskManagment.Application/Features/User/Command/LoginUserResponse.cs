namespace TaskManagment.Application.Features.User.Command;

public class LoginUserResponse
{
    public string Token { get; set; } = null!;
    public string Username { get; set; } = null!;

}