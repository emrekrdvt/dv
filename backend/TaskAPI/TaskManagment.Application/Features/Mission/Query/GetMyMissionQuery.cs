using MediatR;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Domain.Enums;

namespace TaskManagment.Application.Features.Mission.Query;

public class GetMyMissionQuery : IRequest<CustomResponse<List<MyMissionDto>>>
{
    public int UserId { get; set; }
    public bool today { get; set; }
}


public class MyMissionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public MyMissionProject MyMissionProject { get; set; }
    public StatusObjMymisson Status { get; set; }

}
 
public class MyMissionProject
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public DateTime createdat { get; set; }
    public MyCustomer Customer { get; set; } = null!;
}
public class MyCustomer 
{
    public string Name { get; set; } = null!;
 }
public class StatusObjMymisson
{
    public MissionStatus Status { get; set; }
    public string StatusName { get; set; }
}