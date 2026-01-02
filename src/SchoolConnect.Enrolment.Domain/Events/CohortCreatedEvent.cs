using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record CohortCreatedEvent : DomainEvent
{
    public Guid StreamId { get; init; }
    public Guid CentreId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Capacity { get; init; }
}
