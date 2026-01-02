using MongoDB.Driver;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.Interfaces;

namespace SchoolConnect.Calendar.Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly IMongoCollection<CalendarEvent> _collection;

    public EventRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<CalendarEvent>("CalendarEvents");
    }

    public async Task<CalendarEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<CalendarEvent>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(e => e.InstituteId == instituteId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CalendarEvent>> GetByCentreIdAsync(Guid centreId, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(e => e.CentreId == centreId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CalendarEvent>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, Guid? instituteId = null, CancellationToken cancellationToken = default)
    {
        var filter = Builders<CalendarEvent>.Filter.And(
            Builders<CalendarEvent>.Filter.Gte(e => e.StartTime, startDate),
            Builders<CalendarEvent>.Filter.Lte(e => e.EndTime, endDate)
        );

        if (instituteId.HasValue)
        {
            filter = Builders<CalendarEvent>.Filter.And(
                filter,
                Builders<CalendarEvent>.Filter.Eq(e => e.InstituteId, instituteId.Value)
            );
        }

        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CalendarEvent>> GetUpcomingEventsAsync(Guid userId, int count = 10, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        return await _collection
            .Find(e => e.StartTime >= now)
            .SortBy(e => e.StartTime)
            .Limit(count)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(calendarEvent, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneAsync(e => e.Id == calendarEvent.Id, calendarEvent, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(e => e.Id == id, cancellationToken);
    }
}
