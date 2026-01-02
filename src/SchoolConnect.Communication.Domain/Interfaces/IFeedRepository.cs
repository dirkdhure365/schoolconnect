using SchoolConnect.Communication.Domain.Entities;

namespace SchoolConnect.Communication.Domain.Interfaces;

public interface IFeedRepository
{
    Task<FeedItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<FeedItem>> GetByUserIdAsync(Guid userId, int page = 1, int pageSize = 50, CancellationToken cancellationToken = default);
    Task AddAsync(FeedItem feedItem, CancellationToken cancellationToken = default);
    Task UpdateAsync(FeedItem feedItem, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
