using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Application.Services;

public interface IConflictDetectionService
{
    Task<bool> HasTeacherConflictAsync(
        Guid teacherId,
        Guid timetableId,
        DayOfWeek dayOfWeek,
        Guid periodId,
        Guid? excludeSlotId = null,
        CancellationToken cancellationToken = default);

    Task<bool> HasFacilityConflictAsync(
        Guid facilityId,
        Guid timetableId,
        DayOfWeek dayOfWeek,
        Guid periodId,
        Guid? excludeSlotId = null,
        CancellationToken cancellationToken = default);

    Task<bool> HasClassConflictAsync(
        Guid classId,
        Guid timetableId,
        DayOfWeek dayOfWeek,
        Guid periodId,
        Guid? excludeSlotId = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TimetableSlot>> DetectConflictsAsync(
        Guid timetableId,
        CancellationToken cancellationToken = default);
}
