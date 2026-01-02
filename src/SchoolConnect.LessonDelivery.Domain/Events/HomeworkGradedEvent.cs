using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record HomeworkGradedEvent : DomainEvent
{
    public Guid HomeworkId { get; init; }
    public Guid StudentId { get; init; }
    public decimal MarksObtained { get; init; }
    public Guid GradedBy { get; init; }
}
