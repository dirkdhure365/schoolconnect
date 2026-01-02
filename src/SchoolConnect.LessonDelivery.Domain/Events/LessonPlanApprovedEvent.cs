using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record LessonPlanApprovedEvent : DomainEvent
{
    public Guid ApprovedBy { get; init; }
    public string? Notes { get; init; }
}
