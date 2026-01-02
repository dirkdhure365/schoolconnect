using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Institution.Domain.ValueObjects;

public class WorkingHours : ValueObject
{
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public List<DayOfWeek> WorkingDays { get; private set; }

    private WorkingHours() 
    {
        WorkingDays = [];
    }

    public WorkingHours(TimeOnly startTime, TimeOnly endTime, List<DayOfWeek> workingDays)
    {
        StartTime = startTime;
        EndTime = endTime;
        WorkingDays = workingDays;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartTime;
        yield return EndTime;
        foreach (var day in WorkingDays.OrderBy(d => d))
        {
            yield return day;
        }
    }
}
