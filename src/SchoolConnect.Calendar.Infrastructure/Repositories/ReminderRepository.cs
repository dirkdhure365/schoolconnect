using MongoDB.Driver;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.Interfaces;
using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Infrastructure.Repositories;

public class ReminderRepository : IReminderRepository
{
    private readonly IMongoCollection<EventReminder> _collection;

    public ReminderRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<EventReminder>("EventReminders");
    }

    public async Task<EventReminder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(r => r.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<EventReminder>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(r => r.EventId == eventId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<EventReminder>> GetPendingRemindersAsync(DateTime upToTime, CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(r => r.Status == ReminderStatus.Pending && r.ReminderTime <= upToTime)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(EventReminder reminder, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(reminder, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(EventReminder reminder, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneAsync(r => r.Id == reminder.Id, reminder, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(r => r.Id == id, cancellationToken);
    }
}
