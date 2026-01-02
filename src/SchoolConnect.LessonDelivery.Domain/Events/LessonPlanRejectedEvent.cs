using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record LessonPlanRejectedEvent : DomainEvent
{
    public Guid RejectedBy { get; init; }
    public string? Reason { get; init; }
}
