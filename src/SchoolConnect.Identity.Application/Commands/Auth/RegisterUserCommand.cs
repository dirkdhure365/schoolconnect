using SchoolConnect.Common.Application.Interfaces;
using SchoolConnect.Common.Application.Models;
using SchoolConnect.Identity.Application.DTOs;

namespace SchoolConnect.Identity.Application.Commands.Auth;

public record RegisterUserCommand(
    string Email,
    string Password,
    string UserType,
    string FirstName,
    string LastName,
    string? PhoneNumber = null
) : ICommand<UserDto>;
