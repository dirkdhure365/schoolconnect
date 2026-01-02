using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Domain.Entities;

public class TimetablePeriod : Entity
{
    public Guid TimetableId { get; private set; }
    
    public string Name { get; private set; } = string.Empty;
    public int PeriodNumber { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public int DurationMinutes => (int)(EndTime - StartTime).TotalMinutes;
    
    public PeriodType Type { get; private set; }
    public List<DayOfWeek> ApplicableDays { get; private set; } = [];
    
    public string? Color { get; private set; }

    private TimetablePeriod() { }

    public static TimetablePeriod Create(
        Guid timetableId,
        string name,
        int periodNumber,
        TimeOnly startTime,
        TimeOnly endTime,
        PeriodType type = PeriodType.Lesson,
        List<DayOfWeek>? applicableDays = null,
        string? color = null)
    {
        return new TimetablePeriod
        {
            Id = Guid.NewGuid(),
            TimetableId = timetableId,
            Name = name,
            PeriodNumber = periodNumber,
            StartTime = startTime,
            EndTime = endTime,
            Type = type,
            ApplicableDays = applicableDays ?? [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday],
            Color = color,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? name = null,
        TimeOnly? startTime = null,
        TimeOnly? endTime = null,
        PeriodType? type = null,
        List<DayOfWeek>? applicableDays = null,
        string? color = null)
    {
        if (name != null) Name = name;
        if (startTime != null) StartTime = startTime.Value;
        if (endTime != null) EndTime = endTime.Value;
        if (type != null) Type = type.Value;
        if (applicableDays != null) ApplicableDays = applicableDays;
        if (color != null) Color = color;

        UpdatedAt = DateTime.UtcNow;
    }
}
