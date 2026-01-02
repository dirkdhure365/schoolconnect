using SchoolConnect.Common.Application.Interfaces;
using SchoolConnect.Common.Application.Models;

namespace SchoolConnect.Identity.Application.Commands.Auth;

public record LogoutCommand(
    Guid UserId,
    string RefreshToken
) : ICommand;
