using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Communication.Domain.Enums;
using SchoolConnect.Communication.Domain.Events;

namespace SchoolConnect.Communication.Domain.Entities;

public class FeedItem : AggregateRoot
{
    public Guid UserId { get; private set; }
    public Guid InstituteId { get; private set; }
    
    public FeedItemType Type { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Summary { get; private set; }
    public string? ImageUrl { get; private set; }
    public FeedItemData? Data { get; private set; }
    
    public Guid SourceId { get; private set; }
    public string SourceType { get; private set; } = string.Empty;
    
    public string? ActionUrl { get; private set; }
    public string? ActionText { get; private set; }
    
    public int Priority { get; private set; }
    public bool IsRead { get; private set; }
    public bool IsDismissed { get; private set; }
    
    public DateTime? ExpiresAt { get; private set; }

    private FeedItem() { }

    public static FeedItem Create(
        Guid userId,
        Guid instituteId,
        FeedItemType type,
        string title,
        Guid sourceId,
        string sourceType,
        string? summary = null,
        string? imageUrl = null,
        FeedItemData? data = null,
        string? actionUrl = null,
        string? actionText = null,
        int priority = 0,
        DateTime? expiresAt = null)
    {
        var feedItem = new FeedItem
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            InstituteId = instituteId,
            Type = type,
            Title = title,
            Summary = summary,
            ImageUrl = imageUrl,
            Data = data,
            SourceId = sourceId,
            SourceType = sourceType,
            ActionUrl = actionUrl,
            ActionText = actionText,
            Priority = priority,
            IsRead = false,
            IsDismissed = false,
            ExpiresAt = expiresAt,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        feedItem.Apply(new FeedItemCreatedEvent(feedItem.Id, userId, type, sourceId));
        return feedItem;
    }

    public void MarkAsRead()
    {
        IsRead = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Dismiss()
    {
        IsDismissed = true;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}

public class FeedItemData
{
    public string EntityType { get; set; } = string.Empty;
    public Guid EntityId { get; set; }
    public Dictionary<string, object> Properties { get; set; } = new();
}
