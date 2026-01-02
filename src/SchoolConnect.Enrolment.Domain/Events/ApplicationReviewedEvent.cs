using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record ApplicationReviewedEvent : DomainEvent
{
    public string ApplicationNumber { get; init; } = string.Empty;
    public Guid ReviewedBy { get; init; }
    public DateTime ReviewedAt { get; init; }
}
