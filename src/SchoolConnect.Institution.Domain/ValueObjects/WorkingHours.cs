using SchoolConnect.Institution.Domain.Primitives;

namespace SchoolConnect.Institution.Domain.ValueObjects;

public class WorkingHours : ValueObject
{
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public List<DayOfWeek> WorkingDays { get; private set; }
    
    private WorkingHours() { 
        WorkingDays = new List<DayOfWeek>();
    }
    
    public WorkingHours(TimeOnly startTime, TimeOnly endTime, List<DayOfWeek> workingDays)
    {
        if (endTime <= startTime)
            throw new ArgumentException("End time must be after start time");
            
        StartTime = startTime;
        EndTime = endTime;
        WorkingDays = workingDays ?? throw new ArgumentNullException(nameof(workingDays));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartTime;
        yield return EndTime;
        foreach (var day in WorkingDays.OrderBy(d => d))
            yield return day;
    }
}
