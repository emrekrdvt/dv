namespace TaskManagment.Domain.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = string.Empty;

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
    public ICollection<Mission> Missions { get; set; } = new List<Mission>();
}
