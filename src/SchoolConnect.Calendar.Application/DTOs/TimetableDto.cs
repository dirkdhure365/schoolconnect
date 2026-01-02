using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Application.DTOs;

public class TimetableDto
{
    public Guid Id { get; set; }
    public Guid InstituteId { get; set; }
    public Guid CentreId { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int AcademicYear { get; set; }
    public int? TermNumber { get; set; }
    
    public DateTime EffectiveFrom { get; set; }
    public DateTime EffectiveTo { get; set; }
    
    public TimetableStatus Status { get; set; }
    public DateTime? PublishedAt { get; set; }
    
    public int PeriodCount { get; set; }
    public int SlotCount { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class TimetablePeriodDto
{
    public Guid Id { get; set; }
    public Guid TimetableId { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public int PeriodNumber { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int DurationMinutes { get; set; }
    
    public PeriodType Type { get; set; }
    public List<DayOfWeek> ApplicableDays { get; set; } = [];
    
    public string? Color { get; set; }
}

public class TimetableSlotDto
{
    public Guid Id { get; set; }
    public Guid TimetableId { get; set; }
    public Guid TimetablePeriodId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    
    public Guid ClassId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    
    public Guid SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string SubjectCode { get; set; } = string.Empty;
    
    public Guid TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    
    public Guid? FacilityId { get; set; }
    public string? FacilityName { get; set; }
    
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
}

public class TimetableChangeDto
{
    public Guid Id { get; set; }
    public Guid TimetableSlotId { get; set; }
    public DateTime OriginalDate { get; set; }
    public ChangeType ChangeType { get; set; }
    
    public Guid OriginalTeacherId { get; set; }
    public string OriginalTeacherName { get; set; } = string.Empty;
    
    public Guid? NewTeacherId { get; set; }
    public string? NewTeacherName { get; set; }
    
    public string Reason { get; set; } = string.Empty;
    public bool NotificationSent { get; set; }
    
    public DateTime CreatedAt { get; set; }
}
