using SchoolConnect.Collaboration.Domain.Entities;

namespace SchoolConnect.Collaboration.Domain.Interfaces;

public interface IWorkspaceRepository
{
    Task<Workspace?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Workspace>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default);
    Task<List<Workspace>> GetByCentreIdAsync(Guid centreId, CancellationToken cancellationToken = default);
    Task<List<Workspace>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Workspace workspace, CancellationToken cancellationToken = default);
    Task UpdateAsync(Workspace workspace, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
