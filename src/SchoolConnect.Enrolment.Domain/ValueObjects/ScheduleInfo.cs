using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.ValueObjects;

public class ScheduleInfo : ValueObject
{
    public List<ScheduleSlot> Slots { get; private set; } = [];

    private ScheduleInfo() { }

    public ScheduleInfo(List<ScheduleSlot> slots)
    {
        Slots = slots ?? [];
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        foreach (var slot in Slots)
            yield return slot;
    }
}

public class ScheduleSlot : ValueObject
{
    public DayOfWeek Day { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }

    private ScheduleSlot() { }

    public ScheduleSlot(DayOfWeek day, TimeOnly startTime, TimeOnly endTime)
    {
        Day = day;
        StartTime = startTime;
        EndTime = endTime;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Day;
        yield return StartTime;
        yield return EndTime;
    }
}
