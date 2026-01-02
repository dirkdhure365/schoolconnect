using SchoolConnect.Common.Application.Interfaces;
using SchoolConnect.Common.Application.Models;
using SchoolConnect.Identity.Application.DTOs;

namespace SchoolConnect.Identity.Application.Commands.Auth;

public record LoginCommand(
    string Email,
    string Password,
    string? DeviceInfo = null,
    string? IpAddress = null
) : ICommand<LoginResultDto>;
