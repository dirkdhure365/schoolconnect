using SchoolConnect.Identity.Application.DTOs;

namespace SchoolConnect.Identity.Application.Services;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, string email, List<string> roles, List<string> permissions);
    string GenerateRefreshToken();
    DateTime GetTokenExpiration();
    Guid? ValidateToken(string token);
}
