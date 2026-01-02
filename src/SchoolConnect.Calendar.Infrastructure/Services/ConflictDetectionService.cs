using SchoolConnect.Calendar.Application.Services;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.Interfaces;

namespace SchoolConnect.Calendar.Infrastructure.Services;

public class ConflictDetectionService : IConflictDetectionService
{
    private readonly ITimetableSlotRepository _slotRepository;

    public ConflictDetectionService(ITimetableSlotRepository slotRepository)
    {
        _slotRepository = slotRepository;
    }

    public async Task<bool> HasTeacherConflictAsync(
        Guid teacherId,
        Guid timetableId,
        DayOfWeek dayOfWeek,
        Guid periodId,
        Guid? excludeSlotId = null,
        CancellationToken cancellationToken = default)
    {
        var slots = await _slotRepository.GetByTeacherIdAsync(teacherId, timetableId, cancellationToken);
        return slots.Any(s =>
            s.DayOfWeek == dayOfWeek &&
            s.TimetablePeriodId == periodId &&
            (!excludeSlotId.HasValue || s.Id != excludeSlotId.Value));
    }

    public async Task<bool> HasFacilityConflictAsync(
        Guid facilityId,
        Guid timetableId,
        DayOfWeek dayOfWeek,
        Guid periodId,
        Guid? excludeSlotId = null,
        CancellationToken cancellationToken = default)
    {
        var slots = await _slotRepository.GetByFacilityIdAsync(facilityId, timetableId, cancellationToken);
        return slots.Any(s =>
            s.DayOfWeek == dayOfWeek &&
            s.TimetablePeriodId == periodId &&
            (!excludeSlotId.HasValue || s.Id != excludeSlotId.Value));
    }

    public async Task<bool> HasClassConflictAsync(
        Guid classId,
        Guid timetableId,
        DayOfWeek dayOfWeek,
        Guid periodId,
        Guid? excludeSlotId = null,
        CancellationToken cancellationToken = default)
    {
        var slots = await _slotRepository.GetByClassIdAsync(classId, timetableId, cancellationToken);
        return slots.Any(s =>
            s.DayOfWeek == dayOfWeek &&
            s.TimetablePeriodId == periodId &&
            (!excludeSlotId.HasValue || s.Id != excludeSlotId.Value));
    }

    public async Task<IEnumerable<TimetableSlot>> DetectConflictsAsync(
        Guid timetableId,
        CancellationToken cancellationToken = default)
    {
        var allSlots = await _slotRepository.GetByTimetableIdAsync(timetableId, cancellationToken);
        var conflictingSlots = new List<TimetableSlot>();

        var slotsList = allSlots.ToList();
        for (int i = 0; i < slotsList.Count; i++)
        {
            for (int j = i + 1; j < slotsList.Count; j++)
            {
                var slot1 = slotsList[i];
                var slot2 = slotsList[j];

                if (slot1.DayOfWeek == slot2.DayOfWeek &&
                    slot1.TimetablePeriodId == slot2.TimetablePeriodId)
                {
                    if (slot1.TeacherId == slot2.TeacherId ||
                        slot1.ClassId == slot2.ClassId ||
                        (slot1.FacilityId.HasValue && slot1.FacilityId == slot2.FacilityId))
                    {
                        if (!conflictingSlots.Contains(slot1))
                            conflictingSlots.Add(slot1);
                        if (!conflictingSlots.Contains(slot2))
                            conflictingSlots.Add(slot2);
                    }
                }
            }
        }

        return conflictingSlots;
    }
}
