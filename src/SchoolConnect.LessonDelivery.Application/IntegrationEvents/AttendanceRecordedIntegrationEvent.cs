using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Application.IntegrationEvents;

public record AttendanceRecordedIntegrationEvent
{
    public Guid AttendanceId { get; init; }
    public Guid LessonSessionId { get; init; }
    public Guid ClassId { get; init; }
    public Guid StudentId { get; init; }
    public string StudentName { get; init; } = string.Empty;
    public AttendanceStatus Status { get; init; }
    public DateTime MarkedAt { get; init; }
    public bool IsExcused { get; init; }
}
