using MongoDB.Driver;
using SchoolConnect.Collaboration.Domain.Entities;
using SchoolConnect.Collaboration.Domain.Interfaces;
using SchoolConnect.Collaboration.Infrastructure.Persistence;

namespace SchoolConnect.Collaboration.Infrastructure.Repositories;

public class WorkspaceRepository : IWorkspaceRepository
{
    private readonly CollaborationDbContext _context;

    public WorkspaceRepository(CollaborationDbContext context)
    {
        _context = context;
    }

    public async Task<Workspace?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Workspaces
            .Find(w => w.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Workspace>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _context.Workspaces
            .Find(w => w.InstituteId == instituteId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Workspace>> GetByCentreIdAsync(Guid centreId, CancellationToken cancellationToken = default)
    {
        return await _context.Workspaces
            .Find(w => w.CentreId == centreId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Workspace>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        // Get workspace IDs where user is a member
        var memberWorkspaceIds = await _context.WorkspaceMembers
            .Find(m => m.UserId == userId)
            .Project(m => m.WorkspaceId)
            .ToListAsync(cancellationToken);

        if (!memberWorkspaceIds.Any())
            return new List<Workspace>();

        return await _context.Workspaces
            .Find(w => memberWorkspaceIds.Contains(w.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Workspace workspace, CancellationToken cancellationToken = default)
    {
        await _context.Workspaces.InsertOneAsync(workspace, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Workspace workspace, CancellationToken cancellationToken = default)
    {
        await _context.Workspaces.ReplaceOneAsync(
            w => w.Id == workspace.Id,
            workspace,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Workspaces.DeleteOneAsync(w => w.Id == id, cancellationToken);
    }
}
