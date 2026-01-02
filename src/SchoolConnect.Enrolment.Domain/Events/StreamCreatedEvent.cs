using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record StreamCreatedEvent : DomainEvent
{
    public Guid InstituteId { get; init; }
    public Guid ProgramOfferingId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Year { get; init; }
}
