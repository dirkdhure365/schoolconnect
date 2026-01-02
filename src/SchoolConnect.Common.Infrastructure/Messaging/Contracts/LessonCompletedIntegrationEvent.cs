namespace SchoolConnect.Common.Infrastructure.Messaging.Contracts;

public record LessonCompletedIntegrationEvent : IntegrationEvent
{
    public Guid LessonId { get; init; }
    public string LessonTitle { get; init; } = string.Empty;
    public Guid SubjectId { get; init; }
    public Guid TeacherId { get; init; }
    public DateTime CompletionDate { get; init; }
    public int DurationMinutes { get; init; }
}
