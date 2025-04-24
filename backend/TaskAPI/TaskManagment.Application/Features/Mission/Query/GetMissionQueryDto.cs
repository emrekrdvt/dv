using TaskManagment.Domain.Enums;

namespace TaskManagment.Application.Features.Mission.Query;

public class GetMissionQueryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public StatusObj Status { get; set; }
    public ProjectUserDto AssignedUser { get; set; } = new();
}
 public class ProjectUserDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
}

public class StatusObj
{
    public MissionStatus Status { get; set; }
    public string StatusName { get; set; }
}