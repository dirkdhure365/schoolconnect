using SchoolConnect.Calendar.Application.Services;
using SchoolConnect.Calendar.Domain.Interfaces;
using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Infrastructure.Services;

public class ConflictDetectionService : IConflictDetectionService
{
    private readonly ITimetableSlotRepository _slotRepository;

    public ConflictDetectionService(ITimetableSlotRepository slotRepository)
    {
        _slotRepository = slotRepository;
    }

    public async Task<IEnumerable<ConflictInfo>> DetectTimetableConflictsAsync(Guid timetableId, CancellationToken cancellationToken = default)
    {
        var conflicts = new List<ConflictInfo>();
        var slots = await _slotRepository.GetByTimetableIdAsync(timetableId, cancellationToken);
        var slotsList = slots.ToList();

        foreach (var slot in slotsList)
        {
            var conflictingSlots = slotsList
                .Where(s => s.Id != slot.Id && 
                           s.DayOfWeek == slot.DayOfWeek && 
                           s.TimetablePeriodId == slot.TimetablePeriodId)
                .ToList();

            foreach (var conflictingSlot in conflictingSlots)
            {
                if (slot.TeacherId == conflictingSlot.TeacherId)
                {
                    conflicts.Add(new ConflictInfo
                    {
                        Type = ConflictType.TeacherDoubleBooked,
                        Description = $"Teacher {slot.TeacherName} is double booked",
                        TeacherId = slot.TeacherId,
                        Day = slot.DayOfWeek,
                        PeriodId = slot.TimetablePeriodId
                    });
                }

                if (slot.FacilityId.HasValue && slot.FacilityId == conflictingSlot.FacilityId)
                {
                    conflicts.Add(new ConflictInfo
                    {
                        Type = ConflictType.FacilityDoubleBooked,
                        Description = $"Facility {slot.FacilityName} is double booked",
                        FacilityId = slot.FacilityId,
                        Day = slot.DayOfWeek,
                        PeriodId = slot.TimetablePeriodId
                    });
                }

                if (slot.ClassId == conflictingSlot.ClassId)
                {
                    conflicts.Add(new ConflictInfo
                    {
                        Type = ConflictType.ClassDoubleBooked,
                        Description = $"Class {slot.ClassName} is double booked",
                        ClassId = slot.ClassId,
                        Day = slot.DayOfWeek,
                        PeriodId = slot.TimetablePeriodId
                    });
                }
            }
        }

        return conflicts;
    }

    public async Task<bool> HasTeacherConflictAsync(Guid teacherId, Guid timetableId, DayOfWeek day, Guid periodId, CancellationToken cancellationToken = default)
    {
        var slots = await _slotRepository.GetByTeacherIdAsync(teacherId, timetableId, cancellationToken);
        return slots.Any(s => s.DayOfWeek == day && s.TimetablePeriodId == periodId);
    }

    public async Task<bool> HasFacilityConflictAsync(Guid facilityId, Guid timetableId, DayOfWeek day, Guid periodId, CancellationToken cancellationToken = default)
    {
        var slots = await _slotRepository.GetByFacilityIdAsync(facilityId, timetableId, cancellationToken);
        return slots.Any(s => s.DayOfWeek == day && s.TimetablePeriodId == periodId);
    }

    public async Task<bool> HasClassConflictAsync(Guid classId, Guid timetableId, DayOfWeek day, Guid periodId, CancellationToken cancellationToken = default)
    {
        var slots = await _slotRepository.GetByClassIdAsync(classId, timetableId, cancellationToken);
        return slots.Any(s => s.DayOfWeek == day && s.TimetablePeriodId == periodId);
    }
}
