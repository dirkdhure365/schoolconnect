using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record ClassCreatedEvent : DomainEvent
{
    public Guid CohortId { get; init; }
    public Guid SubjectId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Capacity { get; init; }
}
