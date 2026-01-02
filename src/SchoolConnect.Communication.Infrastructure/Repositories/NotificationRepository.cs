using MongoDB.Driver;
using SchoolConnect.Communication.Domain.Entities;
using SchoolConnect.Communication.Domain.Interfaces;
using SchoolConnect.Communication.Infrastructure.Persistence;

namespace SchoolConnect.Communication.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly CommunicationDbContext _context;

    public NotificationRepository(CommunicationDbContext context)
    {
        _context = context;
    }

    public async Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Notifications
            .Find(n => n.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Notification>> GetByUserIdAsync(
        Guid userId, 
        int page = 1, 
        int pageSize = 50, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Notifications
            .Find(n => n.UserId == userId)
            .SortByDescending(n => n.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return (int)await _context.Notifications
            .CountDocumentsAsync(
                n => n.UserId == userId && n.ReadAt == null,
                cancellationToken: cancellationToken);
    }

    public async Task AddAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        await _context.Notifications.InsertOneAsync(notification, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        await _context.Notifications.ReplaceOneAsync(
            n => n.Id == notification.Id,
            notification,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Notifications.DeleteOneAsync(n => n.Id == id, cancellationToken);
    }

    public async Task MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Notification>.Filter.And(
            Builders<Notification>.Filter.Eq(n => n.UserId, userId),
            Builders<Notification>.Filter.Eq(n => n.ReadAt, null)
        );

        var update = Builders<Notification>.Update.Set(n => n.ReadAt, DateTime.UtcNow);

        await _context.Notifications.UpdateManyAsync(filter, update, cancellationToken: cancellationToken);
    }
}
