namespace TaskManagment.Application.Interfaces.Services;

public interface IHashService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}