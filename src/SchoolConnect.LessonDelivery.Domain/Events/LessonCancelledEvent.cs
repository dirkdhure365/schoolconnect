using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record LessonCancelledEvent : DomainEvent
{
    public string? Reason { get; init; }
}
