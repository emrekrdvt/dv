using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.User;

public class UsersQuery : IRequest<CustomResponse<IEnumerable<UserQueryDTO>>>
{
    public int Id { get; set; }
}