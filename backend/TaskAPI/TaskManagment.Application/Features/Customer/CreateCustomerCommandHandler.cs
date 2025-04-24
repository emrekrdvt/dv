using MediatR;
using TaskManagment.Application.Features.Admin;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Customer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomResponse<IEnumerable<Domain.Entities.Customer>>>
{
    
    private readonly IGenericRepository<Domain.Entities.Customer> _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(IGenericRepository<Domain.Entities.Customer> customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }
    

    public async Task<CustomResponse<IEnumerable<Domain.Entities.Customer>>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = new Domain.Entities.Customer
            {
                Name = request.name
            };

            _customerRepository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var customers = await _customerRepository.GetAllAsync();
            if (customers == null)
            {
                return CustomResponse<IEnumerable<Domain.Entities.Customer>>.FailResponse("hata");
            }
            return CustomResponse<IEnumerable<Domain.Entities.Customer>>.SuccessResponse(customers, "ok");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CustomResponse<IEnumerable<Domain.Entities.Customer>>.FailResponse("Hata");
        }
    }
}