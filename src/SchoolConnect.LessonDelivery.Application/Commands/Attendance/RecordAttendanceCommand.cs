using MediatR;
using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Application.Commands.Attendance;

public record RecordAttendanceCommand : IRequest<Guid>
{
    public Guid LessonSessionId { get; init; }
    public Guid ClassId { get; init; }
    public Guid StudentId { get; init; }
    public string StudentName { get; init; } = string.Empty;
    public string StudentCode { get; init; } = string.Empty;
    public AttendanceStatus Status { get; init; }
    public Guid MarkedBy { get; init; }
    public DateTime? ArrivalTime { get; init; }
    public int? LateByMinutes { get; init; }
    public string? AbsenceReason { get; init; }
    public bool IsExcused { get; init; }
    public string? Notes { get; init; }
}
