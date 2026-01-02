using SchoolConnect.Common.Application.Interfaces;
using SchoolConnect.Common.Application.Models;
using SchoolConnect.Identity.Application.DTOs;

namespace SchoolConnect.Identity.Application.Queries.Users;

public record GetUserByIdQuery(
    Guid UserId
) : IQuery<UserDto?>;
