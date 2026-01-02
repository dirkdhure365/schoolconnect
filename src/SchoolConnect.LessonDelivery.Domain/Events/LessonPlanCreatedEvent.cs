using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record LessonPlanCreatedEvent : DomainEvent
{
    public Guid ClassId { get; init; }
    public Guid TeacherId { get; init; }
    public string Title { get; init; } = string.Empty;
}
