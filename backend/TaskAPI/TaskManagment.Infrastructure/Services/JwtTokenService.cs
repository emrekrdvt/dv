using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskManagment.Application.Interfaces.Services;
using TaskManagment.Domain.Entities;

namespace TaskManagment.Infrastructure.Services;

public class JwtTokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim("username", user.Username),
            new Claim("userid", user.Id.ToString()),
            new Claim("isAdmin", user.IsAdmin.ToString())
        };

        //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var keystr = "aeheheheheheheehehehehehehehehehee";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keystr));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        

        var token = new JwtSecurityToken(
            "EHEHEEEHEHEHEHEHEHEH",
            "EHEHEHEHEHEHEHEHEHEHEHEH",
            claims,
            expires: DateTime.UtcNow.AddDays(10),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
