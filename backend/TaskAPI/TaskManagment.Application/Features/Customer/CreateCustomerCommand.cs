using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Admin;

public class CreateCustomerCommand : IRequest<CustomResponse<IEnumerable<Domain.Entities.Customer>>>
{
    public string name { get; set; }
}