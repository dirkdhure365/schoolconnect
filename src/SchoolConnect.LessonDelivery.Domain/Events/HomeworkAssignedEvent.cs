using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record HomeworkAssignedEvent : DomainEvent
{
    public Guid ClassId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime DueDate { get; init; }
}
