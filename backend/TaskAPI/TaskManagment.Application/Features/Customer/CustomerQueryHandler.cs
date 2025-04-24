using MediatR;
using TaskManagment.Application.Features.User;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Application.Interfaces.Services;

namespace TaskManagment.Application.Features.Customer;

public class CustomerQueryHandler : IRequestHandler<CustomerQuery, CustomResponse<IEnumerable<Domain.Entities.Customer>>>
{
    private readonly IGenericRepository<Domain.Entities.Customer>  _customerRepository;
    private readonly ITokenService _tokenService;
    private readonly IHashService _hashService;

    public CustomerQueryHandler(IGenericRepository<Domain.Entities.Customer>  customerRepository, ITokenService tokenService,IHashService hashService)
    {
        _customerRepository = customerRepository;
        _tokenService = tokenService;
        _hashService = hashService;
    }



    public async Task<CustomResponse<IEnumerable<Domain.Entities.Customer>>> Handle(CustomerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.id == -1)
            {
                var customer = await _customerRepository.GetAllAsync();
                
                return CustomResponse<IEnumerable<Domain.Entities.Customer>>.SuccessResponse(customer, "ok");    
            }
            
            return CustomResponse<IEnumerable<Domain.Entities.Customer>>.FailResponse("hata");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}