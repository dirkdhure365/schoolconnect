using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record LessonRescheduledEvent : DomainEvent
{
    public DateTime OldStart { get; init; }
    public DateTime NewStart { get; init; }
    public DateTime NewEnd { get; init; }
}
