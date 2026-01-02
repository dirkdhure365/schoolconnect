using SchoolConnect.Identity.Domain.Entities;

namespace SchoolConnect.Identity.Domain.Interfaces;

public interface ISessionRepository
{
    Task<Session?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Session>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<List<Session>> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Session> AddAsync(Session session, CancellationToken cancellationToken = default);
    Task UpdateAsync(Session session, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
