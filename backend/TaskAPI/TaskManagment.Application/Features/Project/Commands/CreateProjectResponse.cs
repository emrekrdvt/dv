namespace TaskManagment.Application.Features.Project;

public class CreateProjectResponse
{
    public int ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public List<int> MissionIds { get; set; } = new();
    public List<int> ProjectUserIds { get; set; } = new();
}