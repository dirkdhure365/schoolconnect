using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.LessonDelivery.Domain.Enums;
using SchoolConnect.LessonDelivery.Domain.ValueObjects;

namespace SchoolConnect.LessonDelivery.Domain.Entities;

public class LessonPlanActivity : Entity
{
    public Guid LessonPlanId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public ActivityType Type { get; private set; }
    public int DurationMinutes { get; private set; }
    public int OrderIndex { get; private set; }
    public List<ActivityMaterial> Materials { get; private set; } = [];
    public string? TeacherInstructions { get; private set; }
    public string? StudentInstructions { get; private set; }
    public string? ExpectedOutcome { get; private set; }

    private LessonPlanActivity() { }

    public static LessonPlanActivity Create(
        Guid lessonPlanId,
        string title,
        ActivityType type,
        int durationMinutes,
        int orderIndex,
        string? description = null)
    {
        return new LessonPlanActivity
        {
            LessonPlanId = lessonPlanId,
            Title = title,
            Description = description,
            Type = type,
            DurationMinutes = durationMinutes,
            OrderIndex = orderIndex
        };
    }

    public void Update(
        string title,
        string? description,
        ActivityType type,
        int durationMinutes,
        int orderIndex)
    {
        Title = title;
        Description = description;
        Type = type;
        DurationMinutes = durationMinutes;
        OrderIndex = orderIndex;
        UpdatedAt = DateTime.UtcNow;
    }
}
