using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Communication.Domain.Entities;

public class ConversationParticipant : Entity
{
    public Guid ConversationId { get; private set; }
    public Guid UserId { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public string? AvatarUrl { get; private set; }
    public string? Role { get; private set; } // Admin, Member
    
    public DateTime JoinedAt { get; private set; }
    public DateTime? LeftAt { get; private set; }
    public bool IsActive { get; private set; }
    
    public Guid? LastReadMessageId { get; private set; }
    public DateTime? LastReadAt { get; private set; }
    public int UnreadCount { get; private set; }
    
    public bool IsMuted { get; private set; }
    public DateTime? MutedUntil { get; private set; }

    private ConversationParticipant() { }

    public static ConversationParticipant Create(
        Guid conversationId,
        Guid userId,
        string userName,
        string? avatarUrl = null,
        string? role = null)
    {
        return new ConversationParticipant
        {
            Id = Guid.NewGuid(),
            ConversationId = conversationId,
            UserId = userId,
            UserName = userName,
            AvatarUrl = avatarUrl,
            Role = role ?? "Member",
            JoinedAt = DateTime.UtcNow,
            IsActive = true,
            UnreadCount = 0,
            IsMuted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void UpdateLastRead(Guid messageId)
    {
        LastReadMessageId = messageId;
        LastReadAt = DateTime.UtcNow;
        UnreadCount = 0;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementUnreadCount()
    {
        UnreadCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Mute(DateTime? until = null)
    {
        IsMuted = true;
        MutedUntil = until;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Unmute()
    {
        IsMuted = false;
        MutedUntil = null;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Leave()
    {
        IsActive = false;
        LeftAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
