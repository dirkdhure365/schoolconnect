using SchoolConnect.Common.Application.Interfaces;
using SchoolConnect.Identity.Application.DTOs;

namespace SchoolConnect.Identity.Application.Queries.Roles;

public record GetRolesQuery() : IQuery<List<RoleDto>>;
