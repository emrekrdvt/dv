using TaskManagment.Domain.Enums;

namespace TaskManagment.Domain.Entities;
public class Mission : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public MissionStatus Status { get; set; } = MissionStatus.Bekliyor;

    public int UserId { get; set; }
    public User? CreatedBy { get; set; } = null!;

    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public int? AssignedUserId { get; set; }
    public User? AssignedUser { get; set; }

    public bool IsDeleted { get; set; } = false;
    public ICollection<History> HistoryRecords { get; set; } = new List<History>();
}

