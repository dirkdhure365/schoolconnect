using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record StudentWithdrawnEvent : DomainEvent
{
    public Guid StudentId { get; init; }
    public string Reason { get; init; } = string.Empty;
    public DateTime WithdrawnAt { get; init; }
}
