using TaskManagment.Domain.Entities;

namespace TaskManagment.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}