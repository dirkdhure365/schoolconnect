using SchoolConnect.Common.Application.Interfaces;
using SchoolConnect.Common.Application.Models;
using SchoolConnect.Identity.Application.DTOs;

namespace SchoolConnect.Identity.Application.Commands.Roles;

public record CreateRoleCommand(
    string Name,
    string? Description = null,
    List<Guid>? PermissionIds = null
) : ICommand<RoleDto>;
