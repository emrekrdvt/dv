namespace TaskManagment.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public bool IsAdmin { get; set; } = false;


    public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
    public ICollection<Mission> AssignedMissions { get; set; } = new List<Mission>();
}
