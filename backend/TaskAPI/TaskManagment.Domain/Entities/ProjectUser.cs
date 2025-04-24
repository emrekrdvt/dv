using System.Collections;

namespace TaskManagment.Domain.Entities;

public class ProjectUser : BaseEntity
{
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
 
}