using SchoolConnect.Institution.Domain.Entities;

namespace SchoolConnect.Institution.Domain.Interfaces;

public interface ITeamRepository
{
    Task<Team?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Team>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default);
    Task<Team> AddAsync(Team team, CancellationToken cancellationToken = default);
    Task UpdateAsync(Team team, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TeamMember>> GetTeamMembersAsync(Guid teamId, CancellationToken cancellationToken = default);
    Task<TeamMember> AddTeamMemberAsync(TeamMember teamMember, CancellationToken cancellationToken = default);
    Task RemoveTeamMemberAsync(Guid teamId, Guid memberId, CancellationToken cancellationToken = default);
}
