namespace SchoolConnect.Calendar.Domain.ValueObjects;

public class TimetableSettings
{
    public TimeOnly DayStartTime { get; set; }
    public TimeOnly DayEndTime { get; set; }
    public int DefaultPeriodDurationMinutes { get; set; }
    public int BreakDurationMinutes { get; set; }
    public List<DayOfWeek> WorkingDays { get; set; } = [];
    public bool AllowDoubleBooking { get; set; }
    public bool RequireFacility { get; set; }

    public static TimetableSettings CreateDefault()
    {
        return new TimetableSettings
        {
            DayStartTime = new TimeOnly(8, 0),
            DayEndTime = new TimeOnly(15, 0),
            DefaultPeriodDurationMinutes = 45,
            BreakDurationMinutes = 15,
            WorkingDays = [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday],
            AllowDoubleBooking = false,
            RequireFacility = false
        };
    }
}
