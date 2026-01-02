using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record AdmissionPeriodOpenedEvent : DomainEvent
{
    public Guid InstituteId { get; init; }
    public string Name { get; init; } = string.Empty;
}
