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
    
    public TimetableSettingsDto Settings { get; set; } = new();
    
    public int PeriodCount { get; set; }
    public int SlotCount { get; set; }
    
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class TimetableSettingsDto
{
    public TimeOnly DayStartTime { get; set; }
    public TimeOnly DayEndTime { get; set; }
    public int DefaultPeriodDurationMinutes { get; set; }
    public int BreakDurationMinutes { get; set; }
    public List<DayOfWeek> WorkingDays { get; set; } = [];
    public bool AllowDoubleBooking { get; set; }
    public bool RequireFacility { get; set; }
}
