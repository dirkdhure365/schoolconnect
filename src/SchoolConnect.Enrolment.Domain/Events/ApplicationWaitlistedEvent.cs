using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record ApplicationWaitlistedEvent : DomainEvent
{
    public string ApplicationNumber { get; init; } = string.Empty;
    public int WaitlistPosition { get; init; }
    public DateTime WaitlistedAt { get; init; }
}
