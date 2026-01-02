using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Application.DTOs;

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
