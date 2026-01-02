using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record ApplicationSubmittedEvent : DomainEvent
{
    public Guid AdmissionPeriodId { get; init; }
    public string ApplicationNumber { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public Guid ProgramOfferingId { get; init; }
    public DateTime SubmittedAt { get; init; }
}
