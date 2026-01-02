using SchoolConnect.Common.Application.Interfaces;
using SchoolConnect.Identity.Application.DTOs;

namespace SchoolConnect.Identity.Application.Queries.Permissions;

public record GetPermissionsQuery() : IQuery<List<PermissionDto>>;
