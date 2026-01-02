using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Communication.Domain.Enums;
using SchoolConnect.Communication.Domain.Events;
using SchoolConnect.Communication.Domain.ValueObjects;

namespace SchoolConnect.Communication.Domain.Entities;

public class Notification : AggregateRoot
{
    public Guid UserId { get; private set; }
    public Guid? InstituteId { get; private set; }
    
    public NotificationType Type { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Body { get; private set; } = string.Empty;
    public string? ImageUrl { get; private set; }
    public NotificationData? Data { get; private set; }
    
    public string? ActionUrl { get; private set; }
    public string? ActionText { get; private set; }
    
    public List<NotificationChannel> ChannelsSent { get; private set; } = [];
    public Dictionary<NotificationChannel, DateTime> DeliveryStatus { get; private set; } = new();
    
    public NotificationStatus Status { get; private set; }
    public DateTime? ReadAt { get; private set; }
    public DateTime? DismissedAt { get; private set; }
    public DateTime? ExpiresAt { get; private set; }
    
    public Guid? SourceId { get; private set; }
    public string? SourceType { get; private set; }
    public string? CorrelationId { get; private set; }

    private Notification() { }

    public static Notification Create(
        Guid userId,
        NotificationType type,
        string title,
        string body,
        List<NotificationChannel> channels,
        Guid? instituteId = null,
        string? imageUrl = null,
        NotificationData? data = null,
        string? actionUrl = null,
        string? actionText = null,
        DateTime? expiresAt = null,
        Guid? sourceId = null,
        string? sourceType = null,
        string? correlationId = null)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            InstituteId = instituteId,
            Type = type,
            Title = title,
            Body = body,
            ImageUrl = imageUrl,
            Data = data,
            ActionUrl = actionUrl,
            ActionText = actionText,
            ChannelsSent = channels,
            Status = NotificationStatus.Pending,
            ExpiresAt = expiresAt,
            SourceId = sourceId,
            SourceType = sourceType,
            CorrelationId = correlationId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        notification.Apply(new NotificationSentEvent(notification.Id, userId, type, channels));
        return notification;
    }

    public void MarkAsRead()
    {
        if (Status != NotificationStatus.Read)
        {
            Status = NotificationStatus.Read;
            ReadAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Apply(new NotificationReadEvent(Id, UserId));
        }
    }

    public void MarkAsSent()
    {
        Status = NotificationStatus.Sent;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsDelivered(NotificationChannel channel)
    {
        DeliveryStatus[channel] = DateTime.UtcNow;
        Status = NotificationStatus.Delivered;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed()
    {
        Status = NotificationStatus.Failed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Dismiss()
    {
        Status = NotificationStatus.Dismissed;
        DismissedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}
