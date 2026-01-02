using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Application.DTOs;

public class TimetableSlotDto
{
    public Guid Id { get; set; }
    public Guid TimetableId { get; set; }
    public Guid TimetablePeriodId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    
    public Guid ClassId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public Guid CohortId { get; set; }
    public string CohortName { get; set; } = string.Empty;
    
    public Guid SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string SubjectCode { get; set; } = string.Empty;
    
    public Guid TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    
    public Guid? FacilityId { get; set; }
    public string? FacilityName { get; set; }
    
    public string? Notes { get; set; }
    public string? Color { get; set; }
    
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
