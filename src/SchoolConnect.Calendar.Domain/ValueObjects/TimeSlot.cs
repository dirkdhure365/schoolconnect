using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.ValueObjects;

public class TimeSlot : ValueObject
{
    public DayOfWeek Day { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }

    private TimeSlot() { }

    public static TimeSlot Create(DayOfWeek day, TimeOnly startTime, TimeOnly endTime)
    {
        return new TimeSlot
        {
            Day = day,
            StartTime = startTime,
            EndTime = endTime
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Day;
        yield return StartTime;
        yield return EndTime;
    }
}
