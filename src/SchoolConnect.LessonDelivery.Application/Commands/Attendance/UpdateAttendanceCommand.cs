using MediatR;
using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Application.Commands.Attendance;

public record UpdateAttendanceCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public AttendanceStatus Status { get; init; }
    public DateTime? ArrivalTime { get; init; }
    public int? LateByMinutes { get; init; }
    public string? AbsenceReason { get; init; }
    public bool? IsExcused { get; init; }
    public string? Notes { get; init; }
}
