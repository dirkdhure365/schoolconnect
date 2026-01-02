using MongoDB.Driver;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.Interfaces;
using SchoolConnect.Calendar.Domain.Enums;
using SchoolConnect.Calendar.Infrastructure.Persistence;

namespace SchoolConnect.Calendar.Infrastructure.Repositories;

public class ReminderRepository : IReminderRepository
{
    private readonly CalendarDbContext _context;

    public ReminderRepository(CalendarDbContext context)
    {
        _context = context;
    }

    public async Task<EventReminder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.EventReminders
            .Find(r => r.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<EventReminder>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return await _context.EventReminders
            .Find(r => r.EventId == eventId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<EventReminder>> GetPendingRemindersAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        return await _context.EventReminders
            .Find(r => r.Status == ReminderStatus.Pending && r.ReminderTime <= now)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(EventReminder reminder, CancellationToken cancellationToken = default)
    {
        await _context.EventReminders.InsertOneAsync(reminder, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(EventReminder reminder, CancellationToken cancellationToken = default)
    {
        await _context.EventReminders.ReplaceOneAsync(
            r => r.Id == reminder.Id,
            reminder,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.EventReminders.DeleteOneAsync(r => r.Id == id, cancellationToken);
    }
}
