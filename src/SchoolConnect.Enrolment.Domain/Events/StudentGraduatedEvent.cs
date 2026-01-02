using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record StudentGraduatedEvent : DomainEvent
{
    public Guid StudentId { get; init; }
    public Guid StreamId { get; init; }
    public DateTime GraduatedAt { get; init; }
}
