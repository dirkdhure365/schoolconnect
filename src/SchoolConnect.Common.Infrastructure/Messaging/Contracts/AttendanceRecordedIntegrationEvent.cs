namespace SchoolConnect.Common.Infrastructure.Messaging.Contracts;

public record AttendanceRecordedIntegrationEvent : IntegrationEvent
{
    public Guid AttendanceId { get; init; }
    public Guid StudentId { get; init; }
    public Guid LessonId { get; init; }
    public DateTime Date { get; init; }
    public string Status { get; init; } = string.Empty; // Present, Absent, Late, Excused
    public string? Notes { get; init; }
}
