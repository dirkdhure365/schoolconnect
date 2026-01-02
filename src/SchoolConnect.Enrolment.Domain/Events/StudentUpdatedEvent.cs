using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record StudentUpdatedEvent : DomainEvent
{
    public string StudentCode { get; init; } = string.Empty;
}
