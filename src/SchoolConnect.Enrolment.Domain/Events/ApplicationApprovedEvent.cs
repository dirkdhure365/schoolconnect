using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record ApplicationApprovedEvent : DomainEvent
{
    public string ApplicationNumber { get; init; } = string.Empty;
    public Guid ApprovedBy { get; init; }
    public DateTime ApprovedAt { get; init; }
}
