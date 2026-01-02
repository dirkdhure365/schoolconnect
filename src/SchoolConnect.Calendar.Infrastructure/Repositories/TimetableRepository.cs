using MongoDB.Driver;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.Interfaces;
using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Infrastructure.Repositories;

public class TimetableRepository : ITimetableRepository
{
    private readonly IMongoCollection<Timetable> _collection;

    public TimetableRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Timetable>("Timetables");
    }

    public async Task<Timetable?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(t => t.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Timetable>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(t => t.InstituteId == instituteId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Timetable>> GetByCentreIdAsync(Guid centreId, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(t => t.CentreId == centreId).ToListAsync(cancellationToken);
    }

    public async Task<Timetable?> GetActiveByInstituteAsync(Guid instituteId, Guid centreId, CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(t => t.InstituteId == instituteId && t.CentreId == centreId && t.Status == TimetableStatus.Published)
            .SortByDescending(t => t.EffectiveFrom)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(Timetable timetable, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(timetable, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Timetable timetable, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneAsync(t => t.Id == timetable.Id, timetable, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(t => t.Id == id, cancellationToken);
    }
}
