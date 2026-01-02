using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Communication.Domain.Enums;
using SchoolConnect.Communication.Domain.Events;

namespace SchoolConnect.Communication.Domain.Entities;

public class Conversation : AggregateRoot
{
    public ConversationType Type { get; private set; }
    public string? Title { get; private set; }
    public string? AvatarUrl { get; private set; }
    
    public Guid? InstituteId { get; private set; }
    public Guid? ClassId { get; private set; }
    
    public List<ConversationParticipant> Participants { get; private set; } = [];
    
    public Guid? LastMessageId { get; private set; }
    public string? LastMessagePreview { get; private set; }
    public Guid? LastMessageSenderId { get; private set; }
    public DateTime? LastMessageAt { get; private set; }
    
    public int MessageCount { get; private set; }
    public bool IsArchived { get; private set; }

    private Conversation() { }

    public static Conversation Create(
        ConversationType type,
        List<ConversationParticipant> participants,
        string? title = null,
        string? avatarUrl = null,
        Guid? instituteId = null,
        Guid? classId = null)
    {
        var conversation = new Conversation
        {
            Id = Guid.NewGuid(),
            Type = type,
            Title = title,
            AvatarUrl = avatarUrl,
            InstituteId = instituteId,
            ClassId = classId,
            Participants = participants,
            MessageCount = 0,
            IsArchived = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var participantIds = participants.Select(p => p.UserId).ToList();
        conversation.Apply(new ConversationCreatedEvent(conversation.Id, type, participantIds));
        return conversation;
    }

    public void AddParticipant(ConversationParticipant participant, Guid addedBy)
    {
        if (!Participants.Any(p => p.UserId == participant.UserId))
        {
            Participants.Add(participant);
            UpdatedAt = DateTime.UtcNow;
            Apply(new ParticipantAddedEvent(Id, participant.UserId, addedBy));
        }
    }

    public void RemoveParticipant(Guid userId, Guid removedBy)
    {
        var participant = Participants.FirstOrDefault(p => p.UserId == userId);
        if (participant != null)
        {
            participant.Leave();
            UpdatedAt = DateTime.UtcNow;
            Apply(new ParticipantRemovedEvent(Id, userId, removedBy));
        }
    }

    public void UpdateLastMessage(Guid messageId, string preview, Guid senderId)
    {
        LastMessageId = messageId;
        LastMessagePreview = preview;
        LastMessageSenderId = senderId;
        LastMessageAt = DateTime.UtcNow;
        MessageCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Archive(Guid archivedBy)
    {
        IsArchived = true;
        UpdatedAt = DateTime.UtcNow;
        Apply(new ConversationArchivedEvent(Id, archivedBy));
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}
