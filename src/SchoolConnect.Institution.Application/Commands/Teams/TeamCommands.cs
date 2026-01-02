using MediatR;
using SchoolConnect.Institution.Domain.DTOs;
using SchoolConnect.Institution.Domain.Enums;

namespace SchoolConnect.Institution.Application.Commands.Teams;

public record CreateTeamCommand(
    Guid InstituteId,
    string Name,
    TeamType Type,
    Guid? CentreId = null,
    string? Description = null,
    Guid? LeaderId = null
) : IRequest<TeamDto>;

public record UpdateTeamCommand(
    Guid Id,
    string Name,
    string? Description = null,
    Guid? LeaderId = null
) : IRequest<TeamDto>;

public record DeleteTeamCommand(
    Guid Id
) : IRequest<bool>;

public record AddTeamMemberCommand(
    Guid TeamId,
    Guid StaffMemberId,
    string? Role = null
) : IRequest<TeamMemberDto>;

public record RemoveTeamMemberCommand(
    Guid TeamId,
    Guid MemberId
) : IRequest<bool>;
