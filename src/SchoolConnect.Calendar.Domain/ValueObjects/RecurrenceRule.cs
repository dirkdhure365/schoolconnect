using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.ValueObjects;

public class RecurrenceRule : ValueObject
{
    public string Frequency { get; private set; } = string.Empty;
    public int Interval { get; private set; }
    public List<DayOfWeek>? DaysOfWeek { get; private set; }
    public List<int>? DaysOfMonth { get; private set; }
    public List<int>? MonthsOfYear { get; private set; }
    public DateTime? EndDate { get; private set; }
    public int? Occurrences { get; private set; }
    public List<DateTime>? ExceptionDates { get; private set; }

    private RecurrenceRule() { }

    public static RecurrenceRule Create(
        string frequency,
        int interval,
        List<DayOfWeek>? daysOfWeek = null,
        List<int>? daysOfMonth = null,
        List<int>? monthsOfYear = null,
        DateTime? endDate = null,
        int? occurrences = null,
        List<DateTime>? exceptionDates = null)
    {
        return new RecurrenceRule
        {
            Frequency = frequency,
            Interval = interval,
            DaysOfWeek = daysOfWeek,
            DaysOfMonth = daysOfMonth,
            MonthsOfYear = monthsOfYear,
            EndDate = endDate,
            Occurrences = occurrences,
            ExceptionDates = exceptionDates
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Frequency;
        yield return Interval;
        yield return EndDate ?? DateTime.MinValue;
        yield return Occurrences ?? 0;
    }
}
