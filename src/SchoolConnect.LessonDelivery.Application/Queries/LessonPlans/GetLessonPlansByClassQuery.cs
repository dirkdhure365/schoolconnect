using MediatR;
using SchoolConnect.LessonDelivery.Application.DTOs;

namespace SchoolConnect.LessonDelivery.Application.Queries.LessonPlans;

public record GetLessonPlansByClassQuery : IRequest<List<LessonPlanSummaryDto>>
{
    public Guid ClassId { get; init; }
}
