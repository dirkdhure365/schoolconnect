using MediatR;

namespace SchoolConnect.LessonDelivery.Application.Commands.LessonPlans;

public record SubmitLessonPlanForApprovalCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public Guid SubmittedBy { get; init; }
}
