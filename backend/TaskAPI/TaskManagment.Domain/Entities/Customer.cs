namespace TaskManagment.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}