using MongoDB.Driver;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.Interfaces;

namespace SchoolConnect.Calendar.Infrastructure.Repositories;

public class TimetableSlotRepository : ITimetableSlotRepository
{
    private readonly IMongoCollection<TimetableSlot> _collection;

    public TimetableSlotRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<TimetableSlot>("TimetableSlots");
    }

    public async Task<TimetableSlot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(s => s.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TimetableSlot>> GetByTimetableIdAsync(Guid timetableId, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(s => s.TimetableId == timetableId && s.IsActive).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TimetableSlot>> GetByTeacherIdAsync(Guid teacherId, Guid timetableId, CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(s => s.TeacherId == teacherId && s.TimetableId == timetableId && s.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TimetableSlot>> GetByClassIdAsync(Guid classId, Guid timetableId, CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(s => s.ClassId == classId && s.TimetableId == timetableId && s.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TimetableSlot>> GetByFacilityIdAsync(Guid facilityId, Guid timetableId, CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(s => s.FacilityId == facilityId && s.TimetableId == timetableId && s.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TimetableSlot slot, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(slot, cancellationToken: cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<TimetableSlot> slots, CancellationToken cancellationToken = default)
    {
        await _collection.InsertManyAsync(slots, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(TimetableSlot slot, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneAsync(s => s.Id == slot.Id, slot, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(s => s.Id == id, cancellationToken);
    }
}
