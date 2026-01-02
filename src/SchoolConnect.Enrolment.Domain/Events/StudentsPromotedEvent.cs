using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record StudentsPromotedEvent : DomainEvent
{
    public List<Guid> StudentIds { get; init; } = [];
    public Guid FromCohortId { get; init; }
    public Guid ToCohortId { get; init; }
    public DateTime PromotedAt { get; init; }
}
