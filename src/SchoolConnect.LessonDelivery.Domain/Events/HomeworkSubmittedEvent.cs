using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record HomeworkSubmittedEvent : DomainEvent
{
    public Guid HomeworkId { get; init; }
    public Guid StudentId { get; init; }
    public DateTime SubmittedAt { get; init; }
}
