using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record CurriculumCoverageUpdatedEvent : DomainEvent
{
    public Guid TopicId { get; init; }
    public decimal ActualHours { get; init; }
}
