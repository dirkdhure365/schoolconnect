using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record HomeworkUpdatedEvent : DomainEvent
{
    public string Title { get; init; } = string.Empty;
}
