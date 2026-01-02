using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record LessonPlanSharedEvent : DomainEvent
{
    public List<Guid> SharedWithTeacherIds { get; init; } = [];
}
