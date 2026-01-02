using SchoolConnect.Collaboration.Domain.Entities;

namespace SchoolConnect.Collaboration.Domain.Interfaces;

public interface IBoardRepository
{
    Task<Board?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Board>> GetByWorkspaceIdAsync(Guid workspaceId, CancellationToken cancellationToken = default);
    Task<List<Board>> GetArchivedByWorkspaceIdAsync(Guid workspaceId, CancellationToken cancellationToken = default);
    Task AddAsync(Board board, CancellationToken cancellationToken = default);
    Task UpdateAsync(Board board, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
