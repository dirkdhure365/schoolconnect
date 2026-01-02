namespace SchoolConnect.Common.Infrastructure.Messaging.Contracts;

public record TimetableChangedIntegrationEvent : IntegrationEvent
{
    public Guid TimetableId { get; init; }
    public Guid SubjectId { get; init; }
    public DateTime OldStartTime { get; init; }
    public DateTime NewStartTime { get; init; }
    public string? OldLocation { get; init; }
    public string? NewLocation { get; init; }
    public string ChangeReason { get; init; } = string.Empty;
    public List<Guid> AffectedStudentIds { get; init; } = [];
}
