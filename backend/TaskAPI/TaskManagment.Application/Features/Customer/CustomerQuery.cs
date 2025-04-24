using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Customer;

public class CustomerQuery :  IRequest<CustomResponse<IEnumerable<Domain.Entities.Customer>>>
{
    public int id { get; set; }
}