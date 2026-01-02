using MongoDB.Driver;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.Interfaces;
using SchoolConnect.Calendar.Infrastructure.Persistence;

namespace SchoolConnect.Calendar.Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly CalendarDbContext _context;

    public EventRepository(CalendarDbContext context)
    {
        _context = context;
    }

    public async Task<CalendarEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Events
            .Find(e => e.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<CalendarEvent>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _context.Events
            .Find(e => e.InstituteId == instituteId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CalendarEvent>> GetByCentreIdAsync(Guid centreId, CancellationToken cancellationToken = default)
    {
        return await _context.Events
            .Find(e => e.CentreId == centreId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CalendarEvent>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await _context.Events
            .Find(e => e.StartTime >= startDate && e.EndTime <= endDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CalendarEvent>> GetUpcomingEventsAsync(Guid userId, int count, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        return await _context.Events
            .Find(e => e.StartTime >= now)
            .SortBy(e => e.StartTime)
            .Limit(count)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default)
    {
        await _context.Events.InsertOneAsync(calendarEvent, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default)
    {
        await _context.Events.ReplaceOneAsync(
            e => e.Id == calendarEvent.Id,
            calendarEvent,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Events.DeleteOneAsync(e => e.Id == id, cancellationToken);
    }
}
