using MediatR;

namespace SchoolConnect.LessonDelivery.Application.Commands.LessonPlans;

public record DeleteLessonPlanCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
}
