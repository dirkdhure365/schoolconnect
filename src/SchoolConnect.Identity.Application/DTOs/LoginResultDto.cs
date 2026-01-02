namespace SchoolConnect.Identity.Application.DTOs;

public class LoginResultDto
{
    public UserDto User { get; set; } = null!;
    public AuthTokenDto Tokens { get; set; } = null!;
    public bool RequiresMfa { get; set; }
}
