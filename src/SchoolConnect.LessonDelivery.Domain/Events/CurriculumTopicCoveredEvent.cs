using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record CurriculumTopicCoveredEvent : DomainEvent
{
    public Guid TopicId { get; init; }
    public Guid ClassId { get; init; }
    public decimal HoursCovered { get; init; }
}
