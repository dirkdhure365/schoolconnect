using SchoolConnect.Collaboration.Domain.Entities;

namespace SchoolConnect.Collaboration.Domain.Interfaces;

public interface ISharedResourceRepository
{
    Task<SharedResource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<SharedResource>> GetByWorkspaceIdAsync(Guid workspaceId, CancellationToken cancellationToken = default);
    Task AddAsync(SharedResource resource, CancellationToken cancellationToken = default);
    Task UpdateAsync(SharedResource resource, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
