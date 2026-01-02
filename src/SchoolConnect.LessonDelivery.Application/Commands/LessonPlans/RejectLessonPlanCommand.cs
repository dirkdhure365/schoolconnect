using MediatR;

namespace SchoolConnect.LessonDelivery.Application.Commands.LessonPlans;

public record RejectLessonPlanCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public Guid RejectedBy { get; init; }
    public string? Reason { get; init; }
}
