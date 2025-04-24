using MediatR;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Application.Interfaces.Services;

namespace TaskManagment.Application.Features.User;


public class UsersQueryHandler : IRequestHandler<UsersQuery, CustomResponse<IEnumerable<UserQueryDTO>>>
{
    
    private readonly IGenericRepository<Domain.Entities.User> _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IHashService _hashService;

    public UsersQueryHandler(IGenericRepository<Domain.Entities.User> userRepository, ITokenService tokenService,IHashService hashService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _hashService = hashService;
    }


    public async Task<CustomResponse<IEnumerable<UserQueryDTO>>> Handle(UsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id == -1)
            {
                var user = await _userRepository.GetAllAsync();
                IEnumerable<UserQueryDTO> usqDto = user.Select(x => new UserQueryDTO()
                {
                    Id = x.Id,
                    email = x.Email,
                    username = x.Username,
                    IsAdmin = x.IsAdmin,
                });
                return CustomResponse<IEnumerable<UserQueryDTO>>.SuccessResponse(usqDto, "ok");    
            }
            
            return CustomResponse<IEnumerable<UserQueryDTO>>.FailResponse("hata");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}