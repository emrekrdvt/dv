namespace TaskManagment.Domain.Entities;

public class History : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int MissionId { get; set; }
    public Mission Mission { get; set; } = null!;

    public string Action { get; set; } = null!; 
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!; 


}