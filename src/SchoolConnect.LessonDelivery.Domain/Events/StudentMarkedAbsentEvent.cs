using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record StudentMarkedAbsentEvent : DomainEvent
{
    public Guid StudentId { get; init; }
    public Guid SessionId { get; init; }
    public string? Reason { get; init; }
}
