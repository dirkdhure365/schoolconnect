using MediatR;
using SchoolConnect.LessonDelivery.Application.DTOs;

namespace SchoolConnect.LessonDelivery.Application.Queries.Sessions;

public record GetLessonSessionByIdQuery : IRequest<LessonSessionDto?>
{
    public Guid Id { get; init; }
}
