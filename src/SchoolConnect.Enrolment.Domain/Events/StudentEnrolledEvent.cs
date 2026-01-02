using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record StudentEnrolledEvent : DomainEvent
{
    public Guid StudentId { get; init; }
    public Guid StreamId { get; init; }
    public Guid CohortId { get; init; }
    public DateTime EnrolledAt { get; init; }
}
