using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record LessonCompletedEvent : DomainEvent
{
    public Guid ScheduledLessonId { get; init; }
    public DateTime ActualEnd { get; init; }
    public int DurationMinutes { get; init; }
}
