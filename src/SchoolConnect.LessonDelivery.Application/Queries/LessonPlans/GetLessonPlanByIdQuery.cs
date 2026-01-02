using MediatR;
using SchoolConnect.LessonDelivery.Application.DTOs;

namespace SchoolConnect.LessonDelivery.Application.Queries.LessonPlans;

public record GetLessonPlanByIdQuery : IRequest<LessonPlanDto?>
{
    public Guid Id { get; init; }
}
