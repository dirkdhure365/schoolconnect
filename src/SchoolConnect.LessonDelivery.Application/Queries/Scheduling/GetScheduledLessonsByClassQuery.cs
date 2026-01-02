using MediatR;
using SchoolConnect.LessonDelivery.Application.DTOs;

namespace SchoolConnect.LessonDelivery.Application.Queries.Scheduling;

public record GetScheduledLessonsByClassQuery : IRequest<List<ScheduledLessonDto>>
{
    public Guid ClassId { get; init; }
}
