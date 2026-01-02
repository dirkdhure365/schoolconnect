namespace SchoolConnect.Calendar.Domain.Entities;

public class TimetableSettings
{
    public TimeOnly DayStartTime { get; set; }
    public TimeOnly DayEndTime { get; set; }
    public int DefaultPeriodDurationMinutes { get; set; }
    public int BreakDurationMinutes { get; set; }
    public List<DayOfWeek> WorkingDays { get; set; } = [];
    public bool AllowDoubleBooking { get; set; }
    public bool RequireFacility { get; set; }
}
