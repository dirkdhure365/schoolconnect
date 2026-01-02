using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Application.Services;

public interface IConflictDetectionService
{
    Task<IEnumerable<ConflictInfo>> DetectTimetableConflictsAsync(Guid timetableId, CancellationToken cancellationToken = default);
    Task<bool> HasTeacherConflictAsync(Guid teacherId, Guid timetableId, DayOfWeek day, Guid periodId, CancellationToken cancellationToken = default);
    Task<bool> HasFacilityConflictAsync(Guid facilityId, Guid timetableId, DayOfWeek day, Guid periodId, CancellationToken cancellationToken = default);
    Task<bool> HasClassConflictAsync(Guid classId, Guid timetableId, DayOfWeek day, Guid periodId, CancellationToken cancellationToken = default);
}

public class ConflictInfo
{
    public ConflictType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid? TeacherId { get; set; }
    public Guid? FacilityId { get; set; }
    public Guid? ClassId { get; set; }
    public DayOfWeek Day { get; set; }
    public Guid PeriodId { get; set; }
}
