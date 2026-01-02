namespace SchoolConnect.Common.Infrastructure.Messaging.Contracts;

public record AnnouncementPublishedIntegrationEvent : IntegrationEvent
{
    public Guid AnnouncementId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string Priority { get; init; } = "Normal"; // Low, Normal, High, Urgent
    public DateTime PublishedDate { get; init; }
    public List<Guid> TargetAudience { get; init; } = []; // Student/Teacher/Parent IDs
}
