namespace SchoolConnect.LessonDelivery.Application.IntegrationEvents;

public record HomeworkAssignedIntegrationEvent
{
    public Guid HomeworkId { get; init; }
    public Guid ClassId { get; init; }
    public string ClassName { get; init; } = string.Empty;
    public Guid SubjectId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime DueDate { get; init; }
    public int? MaxMarks { get; init; }
    public DateTime AssignedAt { get; init; }
}
