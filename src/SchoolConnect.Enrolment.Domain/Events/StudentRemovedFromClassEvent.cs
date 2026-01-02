using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record StudentRemovedFromClassEvent : DomainEvent
{
    public Guid StudentId { get; init; }
    public Guid ClassId { get; init; }
    public DateTime RemovedAt { get; init; }
}
