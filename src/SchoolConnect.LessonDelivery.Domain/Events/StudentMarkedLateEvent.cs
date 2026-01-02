using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record StudentMarkedLateEvent : DomainEvent
{
    public Guid StudentId { get; init; }
    public Guid SessionId { get; init; }
    public int LateByMinutes { get; init; }
}
