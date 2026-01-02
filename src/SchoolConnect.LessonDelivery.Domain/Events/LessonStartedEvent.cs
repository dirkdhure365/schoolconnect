using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record LessonStartedEvent : DomainEvent
{
    public Guid ScheduledLessonId { get; init; }
    public DateTime ActualStart { get; init; }
}
