using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.ValueObjects;

public class LearningObjective : ValueObject
{
    public string Description { get; private set; } = string.Empty;
    public string? BloomLevel { get; private set; }
    public bool IsAssessable { get; private set; }

    private LearningObjective() { }

    public LearningObjective(string description, string? bloomLevel = null, bool isAssessable = false)
    {
        Description = description;
        BloomLevel = bloomLevel;
        IsAssessable = isAssessable;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Description;
        yield return BloomLevel ?? string.Empty;
        yield return IsAssessable;
    }
}
