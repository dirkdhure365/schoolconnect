using MediatR;
using SchoolConnect.Institution.Domain.DTOs;

namespace SchoolConnect.Institution.Application.Queries.Teams;

public record GetTeamByIdQuery(Guid Id) : IRequest<TeamDto?>;

public record GetTeamsByInstituteQuery(Guid InstituteId) : IRequest<IEnumerable<TeamDto>>;

public record GetTeamMembersQuery(Guid TeamId) : IRequest<IEnumerable<TeamMemberDto>>;
