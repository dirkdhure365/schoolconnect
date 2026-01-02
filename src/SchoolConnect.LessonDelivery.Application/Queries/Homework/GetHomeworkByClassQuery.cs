using MediatR;
using SchoolConnect.LessonDelivery.Application.DTOs;

namespace SchoolConnect.LessonDelivery.Application.Queries.Homework;

public record GetHomeworkByClassQuery : IRequest<List<HomeworkDto>>
{
    public Guid ClassId { get; init; }
}
