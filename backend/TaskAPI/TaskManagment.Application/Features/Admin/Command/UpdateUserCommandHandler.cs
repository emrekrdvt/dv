using MediatR;
using TaskManagment.Application.Features.User;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.Admin;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,CustomResponse<UserQueryDTO>>
{
    private readonly IGenericRepository<Domain.Entities.User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(
        IGenericRepository<Domain.Entities.User> userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponse<UserQueryDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.FindAsync(x => x.Id == request.Id);

            if ((bool)request.isDelMethod!)
            {
                _userRepository.Delete(user);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var updatedUserDel=   new UserQueryDTO()
                {
                    Id = 0,
                    email = "",
                    username ="",
                    IsAdmin = false,
                };
                return CustomResponse<UserQueryDTO>.SuccessResponse(updatedUserDel, "ok");

            }
            user!.Username = request.Username ?? user.Username;
            user!.Email = request.Email ?? user.Email;
            user!.IsAdmin = request.Admin && user.IsAdmin;
            
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var userup = await _userRepository.FindAsync(x => x.Id == request.Id);
              var updatedUser=   new UserQueryDTO()
            {
                Id = userup.Id,
                email = userup.Email,
                username = userup.Username,
                IsAdmin = userup.IsAdmin,
            };
            return CustomResponse<UserQueryDTO>.SuccessResponse(updatedUser, "ok");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}