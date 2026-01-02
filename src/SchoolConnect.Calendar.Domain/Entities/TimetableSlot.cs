using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Calendar.Domain.Events;

namespace SchoolConnect.Calendar.Domain.Entities;

public class TimetableSlot : AggregateRoot
{
    public Guid TimetableId { get; private set; }
    public Guid TimetablePeriodId { get; private set; }
    public DayOfWeek DayOfWeek { get; private set; }
    
    public Guid ClassId { get; private set; }
    public string ClassName { get; private set; } = string.Empty;
    public Guid CohortId { get; private set; }
    public string CohortName { get; private set; } = string.Empty;
    
    public Guid SubjectId { get; private set; }
    public string SubjectName { get; private set; } = string.Empty;
    public string SubjectCode { get; private set; } = string.Empty;
    
    public Guid TeacherId { get; private set; }
    public string TeacherName { get; private set; } = string.Empty;
    
    public Guid? FacilityId { get; private set; }
    public string? FacilityName { get; private set; }
    
    public string? Notes { get; private set; }
    public string? Color { get; private set; }
    
    public bool IsActive { get; private set; }

    private TimetableSlot() { }

    public static TimetableSlot Create(
        Guid timetableId,
        Guid periodId,
        DayOfWeek dayOfWeek,
        Guid classId,
        string className,
        Guid cohortId,
        string cohortName,
        Guid subjectId,
        string subjectName,
        string subjectCode,
        Guid teacherId,
        string teacherName,
        Guid? facilityId = null,
        string? facilityName = null,
        string? notes = null,
        string? color = null)
    {
        var slot = new TimetableSlot
        {
            Id = Guid.NewGuid(),
            TimetableId = timetableId,
            TimetablePeriodId = periodId,
            DayOfWeek = dayOfWeek,
            ClassId = classId,
            ClassName = className,
            CohortId = cohortId,
            CohortName = cohortName,
            SubjectId = subjectId,
            SubjectName = subjectName,
            SubjectCode = subjectCode,
            TeacherId = teacherId,
            TeacherName = teacherName,
            FacilityId = facilityId,
            FacilityName = facilityName,
            Notes = notes,
            Color = color,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        slot.Apply(new TimetableSlotCreatedEvent(
            slot.Id,
            timetableId,
            classId,
            teacherId));

        return slot;
    }

    public void Update(
        Guid? teacherId = null,
        string? teacherName = null,
        Guid? facilityId = null,
        string? facilityName = null,
        string? notes = null,
        string? color = null)
    {
        if (teacherId.HasValue) TeacherId = teacherId.Value;
        if (teacherName != null) TeacherName = teacherName;
        if (facilityId.HasValue) FacilityId = facilityId;
        if (facilityName != null) FacilityName = facilityName;
        if (notes != null) Notes = notes;
        if (color != null) Color = color;

        UpdatedAt = DateTime.UtcNow;

        Apply(new TimetableSlotUpdatedEvent(Id, TimetableId));
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}
