using SchoolConnect.Calendar.Domain.Entities;

namespace SchoolConnect.Calendar.Domain.Interfaces;

public interface ITimetableSlotRepository
{
    Task<TimetableSlot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TimetableSlot>> GetByTimetableIdAsync(Guid timetableId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TimetableSlot>> GetByTeacherIdAsync(Guid teacherId, Guid timetableId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TimetableSlot>> GetByClassIdAsync(Guid classId, Guid timetableId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TimetableSlot>> GetByFacilityIdAsync(Guid facilityId, Guid timetableId, CancellationToken cancellationToken = default);
    Task AddAsync(TimetableSlot slot, CancellationToken cancellationToken = default);
    Task UpdateAsync(TimetableSlot slot, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
