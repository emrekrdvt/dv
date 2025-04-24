using MediatR;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Application.Interfaces.Services;

namespace TaskManagment.Application.Features.User.Command;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IGenericRepository<Domain.Entities.User> _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IHashService _hashService;

    public LoginUserCommandHandler(IGenericRepository<Domain.Entities.User> userRepository, ITokenService tokenService,IHashService hashService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _hashService = hashService;
    }

    public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var dbUser = await _userRepository.FindAsync(x => x.Username == request.Username);
            var checkPw = _hashService.VerifyPassword(request.Password, dbUser.PasswordHash);
            if (dbUser == null || !checkPw)
                throw new UnauthorizedAccessException("Geçersiz kullanıcı adı veya şifre.");
            
            var token = _tokenService.GenerateToken(dbUser);

            return new LoginUserResponse
            {
                Username = dbUser.Username,
                Token = token
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}