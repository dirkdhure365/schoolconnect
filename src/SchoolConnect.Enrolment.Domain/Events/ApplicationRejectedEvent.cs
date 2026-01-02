using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record ApplicationRejectedEvent : DomainEvent
{
    public string ApplicationNumber { get; init; } = string.Empty;
    public Guid RejectedBy { get; init; }
    public string Reason { get; init; } = string.Empty;
    public DateTime RejectedAt { get; init; }
}
