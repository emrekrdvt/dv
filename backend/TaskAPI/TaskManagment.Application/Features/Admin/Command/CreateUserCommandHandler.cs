using MediatR;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Application.Interfaces.Services;

namespace TaskManagment.Application.Features.Admin;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CustomResponse<int>>
{
    private readonly IHashService _hashService;
    private readonly IGenericRepository<Domain.Entities.User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        IHashService hashService,
        IGenericRepository<Domain.Entities.User> userRepository,
        IUnitOfWork unitOfWork)
    {
        _hashService = hashService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponse<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var mailExist = await _userRepository.FindAsync(x => x.Email == request.Email || x.Username == request.Username);
        if (mailExist != null)
        {
            return CustomResponse<int>.FailResponse("Bu kullanıcı zaten mevcut.");
        }

        var randomPassword = new Random().Next(100000, 999999).ToString();
        var hashedPassword = _hashService.HashPassword(randomPassword);
        //260100
        //919249
        var user = new Domain.Entities.User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsAdmin = request.Admin
        };

        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Console.WriteLine($"Yeni kullanıcı şifresi: {randomPassword}");

        return CustomResponse<int>.SuccessResponse(user.Id, "Kullanıcı başarıyla oluşturuldu.");
    }
}