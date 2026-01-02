using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record LessonPlanSubmittedForApprovalEvent : DomainEvent
{
    public Guid SubmittedBy { get; init; }
}
