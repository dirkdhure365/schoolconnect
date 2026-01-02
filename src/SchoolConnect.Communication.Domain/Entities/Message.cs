using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Communication.Domain.Enums;
using SchoolConnect.Communication.Domain.Events;
using SchoolConnect.Communication.Domain.ValueObjects;

namespace SchoolConnect.Communication.Domain.Entities;

public class Message : AggregateRoot
{
    public Guid ConversationId { get; private set; }
    public Guid SenderId { get; private set; }
    public string SenderName { get; private set; } = string.Empty;
    public string? SenderAvatarUrl { get; private set; }
    
    public string Content { get; private set; } = string.Empty;
    public List<MessageAttachment> Attachments { get; private set; } = [];
    public MessagePriority Priority { get; private set; }
    
    public Guid? ReplyToMessageId { get; private set; }
    public Guid? ForwardedFromMessageId { get; private set; }
    
    public MessageStatus Status { get; private set; }
    public DateTime SentAt { get; private set; }
    public DateTime? EditedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    
    public List<Guid> ReadByUserIds { get; private set; } = [];
    public Dictionary<Guid, DateTime> ReadReceipts { get; private set; } = new();

    private Message() { }

    public static Message Create(
        Guid conversationId,
        Guid senderId,
        string senderName,
        string content,
        MessagePriority priority = MessagePriority.Normal,
        string? senderAvatarUrl = null,
        List<MessageAttachment>? attachments = null,
        Guid? replyToMessageId = null,
        Guid? forwardedFromMessageId = null)
    {
        var message = new Message
        {
            Id = Guid.NewGuid(),
            ConversationId = conversationId,
            SenderId = senderId,
            SenderName = senderName,
            SenderAvatarUrl = senderAvatarUrl,
            Content = content,
            Attachments = attachments ?? [],
            Priority = priority,
            ReplyToMessageId = replyToMessageId,
            ForwardedFromMessageId = forwardedFromMessageId,
            Status = MessageStatus.Sent,
            SentAt = DateTime.UtcNow,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        message.Apply(new MessageSentEvent(message.Id, conversationId, senderId, content));
        return message;
    }

    public void MarkAsRead(Guid userId)
    {
        if (!ReadByUserIds.Contains(userId))
        {
            ReadByUserIds.Add(userId);
            ReadReceipts[userId] = DateTime.UtcNow;
            Status = MessageStatus.Read;
            UpdatedAt = DateTime.UtcNow;
            Apply(new MessageReadEvent(Id, ConversationId, userId));
        }
    }

    public void Delete(Guid deletedBy)
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        Status = MessageStatus.Deleted;
        UpdatedAt = DateTime.UtcNow;
        Apply(new MessageDeletedEvent(Id, ConversationId, deletedBy));
    }

    public void Edit(string newContent)
    {
        Content = newContent;
        EditedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}
