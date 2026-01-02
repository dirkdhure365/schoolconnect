using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record ParentLinkedToStudentEvent : DomainEvent
{
    public Guid StudentId { get; init; }
    public Guid ParentUserId { get; init; }
    public string Relationship { get; init; } = string.Empty;
}
