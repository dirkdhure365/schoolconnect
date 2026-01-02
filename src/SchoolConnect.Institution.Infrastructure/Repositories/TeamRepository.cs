using MongoDB.Driver;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Interfaces;
using SchoolConnect.Institution.Infrastructure.Persistence;

namespace SchoolConnect.Institution.Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly InstitutionDbContext _context;

    public TeamRepository(InstitutionDbContext context)
    {
        _context = context;
    }

    public async Task<Team?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Teams
            .Find(t => t.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Team>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _context.Teams
            .Find(t => t.InstituteId == instituteId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Team> AddAsync(Team team, CancellationToken cancellationToken = default)
    {
        await _context.Teams.InsertOneAsync(team, cancellationToken: cancellationToken);
        return team;
    }

    public async Task UpdateAsync(Team team, CancellationToken cancellationToken = default)
    {
        await _context.Teams.ReplaceOneAsync(
            t => t.Id == team.Id,
            team,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Teams.DeleteOneAsync(
            t => t.Id == id,
            cancellationToken);
    }

    public async Task<List<TeamMember>> GetTeamMembersAsync(Guid teamId, CancellationToken cancellationToken = default)
    {
        return await _context.TeamMembers
            .Find(m => m.TeamId == teamId && m.LeftAt == null)
            .ToListAsync(cancellationToken);
    }

    public async Task<TeamMember> AddTeamMemberAsync(TeamMember teamMember, CancellationToken cancellationToken = default)
    {
        await _context.TeamMembers.InsertOneAsync(teamMember, cancellationToken: cancellationToken);
        return teamMember;
    }

    public async Task RemoveTeamMemberAsync(Guid teamId, Guid memberId, CancellationToken cancellationToken = default)
    {
        await _context.TeamMembers.DeleteOneAsync(
            m => m.TeamId == teamId && m.Id == memberId,
            cancellationToken);
    }
}
