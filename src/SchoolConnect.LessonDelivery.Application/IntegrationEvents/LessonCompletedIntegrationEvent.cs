namespace SchoolConnect.LessonDelivery.Application.IntegrationEvents;

public record LessonCompletedIntegrationEvent
{
    public Guid LessonSessionId { get; init; }
    public Guid ScheduledLessonId { get; init; }
    public Guid ClassId { get; init; }
    public Guid TeacherId { get; init; }
    public DateTime ActualStart { get; init; }
    public DateTime ActualEnd { get; init; }
    public int DurationMinutes { get; init; }
    public int AttendeeCount { get; init; }
    public int AbsentCount { get; init; }
    public DateTime OccurredAt { get; init; }
}
