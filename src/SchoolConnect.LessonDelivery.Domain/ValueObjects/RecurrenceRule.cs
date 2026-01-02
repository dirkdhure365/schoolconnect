using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.ValueObjects;

public class RecurrenceRule : ValueObject
{
    public string Frequency { get; private set; } = string.Empty;
    public int Interval { get; private set; }
    public List<DayOfWeek>? DaysOfWeek { get; private set; }
    public DateTime? EndDate { get; private set; }
    public int? Occurrences { get; private set; }

    private RecurrenceRule() { }

    public RecurrenceRule(
        string frequency,
        int interval,
        List<DayOfWeek>? daysOfWeek = null,
        DateTime? endDate = null,
        int? occurrences = null)
    {
        Frequency = frequency;
        Interval = interval;
        DaysOfWeek = daysOfWeek;
        EndDate = endDate;
        Occurrences = occurrences;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Frequency;
        yield return Interval;
        yield return DaysOfWeek != null ? string.Join(",", DaysOfWeek) : string.Empty;
        yield return EndDate ?? DateTime.MinValue;
        yield return Occurrences ?? 0;
    }
}
