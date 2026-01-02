using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record TeacherAssignedToClassEvent : DomainEvent
{
    public Guid ClassId { get; init; }
    public Guid TeacherId { get; init; }
    public string TeacherName { get; init; } = string.Empty;
}
