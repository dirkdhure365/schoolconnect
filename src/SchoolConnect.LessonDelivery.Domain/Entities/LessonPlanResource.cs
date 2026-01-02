using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Domain.Entities;

public class LessonPlanResource : Entity
{
    public Guid LessonPlanId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ResourceType Type { get; private set; }
    public string? Url { get; private set; }
    public Guid? FileId { get; private set; }
    public string? Description { get; private set; }
    public bool IsRequired { get; private set; }
    public int OrderIndex { get; private set; }

    private LessonPlanResource() { }

    public static LessonPlanResource Create(
        Guid lessonPlanId,
        string name,
        ResourceType type,
        int orderIndex,
        string? url = null,
        Guid? fileId = null,
        string? description = null,
        bool isRequired = false)
    {
        return new LessonPlanResource
        {
            LessonPlanId = lessonPlanId,
            Name = name,
            Type = type,
            Url = url,
            FileId = fileId,
            Description = description,
            IsRequired = isRequired,
            OrderIndex = orderIndex
        };
    }
}
