using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.ValueObjects;

public class TopicCoverage : ValueObject
{
    public Guid TopicId { get; private set; }
    public string TopicName { get; private set; } = string.Empty;
    public decimal MinutesCovered { get; private set; }
    public bool IsCompleted { get; private set; }
    public string? Notes { get; private set; }

    private TopicCoverage() { }

    public TopicCoverage(Guid topicId, string topicName, decimal minutesCovered, bool isCompleted, string? notes = null)
    {
        TopicId = topicId;
        TopicName = topicName;
        MinutesCovered = minutesCovered;
        IsCompleted = isCompleted;
        Notes = notes;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return TopicId;
        yield return TopicName;
        yield return MinutesCovered;
        yield return IsCompleted;
        yield return Notes ?? string.Empty;
    }
}
