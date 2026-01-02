using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record ExtraCurricularEnrolmentEvent : DomainEvent
{
    public Guid StudentId { get; init; }
    public string ActivityName { get; init; } = string.Empty;
    public DateTime EnrolledAt { get; init; }
}
