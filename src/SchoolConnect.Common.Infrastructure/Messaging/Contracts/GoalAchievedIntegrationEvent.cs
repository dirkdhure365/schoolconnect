namespace SchoolConnect.Common.Infrastructure.Messaging.Contracts;

public record GoalAchievedIntegrationEvent : IntegrationEvent
{
    public Guid GoalId { get; init; }
    public Guid StudentId { get; init; }
    public string GoalTitle { get; init; } = string.Empty;
    public string GoalType { get; init; } = string.Empty; // Academic, Behavioral, etc.
    public DateTime AchievedDate { get; init; }
    public string? Notes { get; init; }
}
