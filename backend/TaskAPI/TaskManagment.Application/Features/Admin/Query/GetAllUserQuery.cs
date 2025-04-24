using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Admin.Query;

public class GetAllUserQuery : IRequest<CustomResponse<List<Domain.Entities.User>>>
{
}