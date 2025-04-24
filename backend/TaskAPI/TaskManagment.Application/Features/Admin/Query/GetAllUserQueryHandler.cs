using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Admin.Query;

public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, CustomResponse<List<Domain.Entities.User>>>
{
    
    
    private readonly IGenericRepository<Domain.Entities.User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUserQueryHandler(
        IGenericRepository<Domain.Entities.User> userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<CustomResponse<List<Domain.Entities.User>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();
        if (users == null)
        {
            return CustomResponse<List<Domain.Entities.User>>.FailResponse("Hata");
        }
        return CustomResponse<List<Domain.Entities.User>>.SuccessResponse(users, "Kullanicilar getirildi");
    }
}