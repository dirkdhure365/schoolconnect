namespace SchoolConnect.EducationSystem.Domain.Events;

public abstract class DomainEvent
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
    public string AggregateId { get; init; } = string.Empty;
    public string EventType { get; init; } = string.Empty;
}
