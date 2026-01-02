using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record LessonPlanUpdatedEvent : DomainEvent
{
    public string Title { get; init; } = string.Empty;
}
