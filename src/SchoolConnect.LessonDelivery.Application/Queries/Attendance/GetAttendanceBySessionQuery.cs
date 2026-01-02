using MediatR;
using SchoolConnect.LessonDelivery.Application.DTOs;

namespace SchoolConnect.LessonDelivery.Application.Queries.Attendance;

public record GetAttendanceBySessionQuery : IRequest<List<AttendanceDto>>
{
    public Guid SessionId { get; init; }
}
