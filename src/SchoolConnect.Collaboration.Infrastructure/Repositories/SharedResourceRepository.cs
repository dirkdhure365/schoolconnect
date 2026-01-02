using MongoDB.Driver;
using SchoolConnect.Collaboration.Domain.Entities;
using SchoolConnect.Collaboration.Domain.Interfaces;
using SchoolConnect.Collaboration.Infrastructure.Persistence;

namespace SchoolConnect.Collaboration.Infrastructure.Repositories;

public class SharedResourceRepository : ISharedResourceRepository
{
    private readonly CollaborationDbContext _context;

    public SharedResourceRepository(CollaborationDbContext context)
    {
        _context = context;
    }

    public async Task<SharedResource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.SharedResources
            .Find(r => r.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<SharedResource>> GetByWorkspaceIdAsync(Guid workspaceId, CancellationToken cancellationToken = default)
    {
        return await _context.SharedResources
            .Find(r => r.WorkspaceId == workspaceId)
            .SortByDescending(r => r.SharedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SharedResource resource, CancellationToken cancellationToken = default)
    {
        await _context.SharedResources.InsertOneAsync(resource, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(SharedResource resource, CancellationToken cancellationToken = default)
    {
        await _context.SharedResources.ReplaceOneAsync(
            r => r.Id == resource.Id,
            resource,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.SharedResources.DeleteOneAsync(r => r.Id == id, cancellationToken);
    }
}
