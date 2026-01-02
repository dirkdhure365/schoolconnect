using SchoolConnect.Common.Application.Interfaces;
using SchoolConnect.Common.Application.Models;

namespace SchoolConnect.Identity.Application.Commands.Auth;

public record ChangePasswordCommand(
    Guid UserId,
    string CurrentPassword,
    string NewPassword
) : ICommand;
