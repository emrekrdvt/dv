namespace TaskManagment.Application.Features.Admin.Query;

public class HistoryQueryDto
{
    public string Action { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string MissionTitle { get; set; } = null!;
    public DateTime Timestamp { get; set; }
}