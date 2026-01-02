using MongoDB.Driver;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.Interfaces;
using SchoolConnect.Calendar.Infrastructure.Persistence;

namespace SchoolConnect.Calendar.Infrastructure.Repositories;

public class TimetableSlotRepository : ITimetableSlotRepository
{
    private readonly CalendarDbContext _context;

    public TimetableSlotRepository(CalendarDbContext context)
    {
        _context = context;
    }

    public async Task<TimetableSlot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.TimetableSlots
            .Find(s => s.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TimetableSlot>> GetByTimetableIdAsync(Guid timetableId, CancellationToken cancellationToken = default)
    {
        return await _context.TimetableSlots
            .Find(s => s.TimetableId == timetableId && s.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TimetableSlot>> GetByTeacherIdAsync(Guid teacherId, Guid timetableId, CancellationToken cancellationToken = default)
    {
        return await _context.TimetableSlots
            .Find(s => s.TeacherId == teacherId && s.TimetableId == timetableId && s.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TimetableSlot>> GetByClassIdAsync(Guid classId, Guid timetableId, CancellationToken cancellationToken = default)
    {
        return await _context.TimetableSlots
            .Find(s => s.ClassId == classId && s.TimetableId == timetableId && s.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TimetableSlot>> GetByFacilityIdAsync(Guid facilityId, Guid timetableId, CancellationToken cancellationToken = default)
    {
        return await _context.TimetableSlots
            .Find(s => s.FacilityId == facilityId && s.TimetableId == timetableId && s.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TimetableSlot slot, CancellationToken cancellationToken = default)
    {
        await _context.TimetableSlots.InsertOneAsync(slot, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(TimetableSlot slot, CancellationToken cancellationToken = default)
    {
        await _context.TimetableSlots.ReplaceOneAsync(
            s => s.Id == slot.Id,
            slot,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.TimetableSlots.DeleteOneAsync(s => s.Id == id, cancellationToken);
    }
}
