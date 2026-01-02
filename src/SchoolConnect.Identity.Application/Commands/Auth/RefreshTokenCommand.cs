using SchoolConnect.Common.Application.Interfaces;
using SchoolConnect.Common.Application.Models;
using SchoolConnect.Identity.Application.DTOs;

namespace SchoolConnect.Identity.Application.Commands.Auth;

public record RefreshTokenCommand(
    string RefreshToken
) : ICommand<AuthTokenDto>;
