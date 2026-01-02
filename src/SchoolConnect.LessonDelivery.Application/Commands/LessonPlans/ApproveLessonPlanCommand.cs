using MediatR;

namespace SchoolConnect.LessonDelivery.Application.Commands.LessonPlans;

public record ApproveLessonPlanCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public Guid ApprovedBy { get; init; }
    public string? Notes { get; init; }
}
