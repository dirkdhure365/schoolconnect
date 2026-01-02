using SchoolConnect.Collaboration.Domain.Entities;

namespace SchoolConnect.Collaboration.Domain.Interfaces;

public interface ICardRepository
{
    Task<Card?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Card>> GetByListIdAsync(Guid listId, CancellationToken cancellationToken = default);
    Task<List<Card>> GetByBoardIdAsync(Guid boardId, CancellationToken cancellationToken = default);
    Task<List<Card>> GetArchivedByBoardIdAsync(Guid boardId, CancellationToken cancellationToken = default);
    Task<List<Card>> GetByAssigneeIdAsync(Guid assigneeId, CancellationToken cancellationToken = default);
    Task<List<Card>> GetDueSoonAsync(DateTime before, CancellationToken cancellationToken = default);
    Task<List<Card>> SearchAsync(Guid boardId, string searchTerm, CancellationToken cancellationToken = default);
    Task AddAsync(Card card, CancellationToken cancellationToken = default);
    Task UpdateAsync(Card card, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
