namespace TaskManagment.Application.Features.User;

public class UserQueryDTO
{
    public int Id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public bool IsAdmin { get; set; }
}