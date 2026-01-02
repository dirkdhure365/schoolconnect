using MongoDB.Driver;
using SchoolConnect.Communication.Domain.Entities;
using SchoolConnect.Communication.Domain.Interfaces;
using SchoolConnect.Communication.Infrastructure.Persistence;

namespace SchoolConnect.Communication.Infrastructure.Repositories;

public class FeedRepository : IFeedRepository
{
    private readonly CommunicationDbContext _context;

    public FeedRepository(CommunicationDbContext context)
    {
        _context = context;
    }

    public async Task<FeedItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.FeedItems
            .Find(f => f.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<FeedItem>> GetByUserIdAsync(
        Guid userId, 
        int page = 1, 
        int pageSize = 50, 
        CancellationToken cancellationToken = default)
    {
        return await _context.FeedItems
            .Find(f => f.UserId == userId && !f.IsDismissed)
            .SortByDescending(f => f.Priority)
            .ThenByDescending(f => f.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(FeedItem feedItem, CancellationToken cancellationToken = default)
    {
        await _context.FeedItems.InsertOneAsync(feedItem, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(FeedItem feedItem, CancellationToken cancellationToken = default)
    {
        await _context.FeedItems.ReplaceOneAsync(
            f => f.Id == feedItem.Id,
            feedItem,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.FeedItems.DeleteOneAsync(f => f.Id == id, cancellationToken);
    }
}
