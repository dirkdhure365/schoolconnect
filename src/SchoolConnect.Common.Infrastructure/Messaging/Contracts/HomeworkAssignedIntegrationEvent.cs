namespace SchoolConnect.Common.Infrastructure.Messaging.Contracts;

public record HomeworkAssignedIntegrationEvent : IntegrationEvent
{
    public Guid HomeworkId { get; init; }
    public Guid SubjectId { get; init; }
    public Guid TeacherId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime AssignedDate { get; init; }
    public DateTime DueDate { get; init; }
    public List<Guid> StudentIds { get; init; } = [];
}
