using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record AdmissionPeriodCreatedEvent : DomainEvent
{
    public Guid InstituteId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int AcademicYear { get; init; }
    public DateTime ApplicationStartDate { get; init; }
    public DateTime ApplicationEndDate { get; init; }
}
