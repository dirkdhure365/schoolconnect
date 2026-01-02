using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Collaboration.Domain.Events;

namespace SchoolConnect.Collaboration.Domain.Entities;

public class CardComment : AggregateRoot
{
    public Guid CardId { get; private set; }
    
    public Guid AuthorId { get; private set; }
    public string AuthorName { get; private set; } = string.Empty;
    public string? AuthorAvatarUrl { get; private set; }
    
    public string Content { get; private set; } = string.Empty;
    public List<string> AttachmentUrls { get; private set; } = [];
    public List<Guid> MentionedUserIds { get; private set; } = [];
    
    public bool IsEdited { get; private set; }
    public DateTime? EditedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    private CardComment() { }

    public static CardComment Create(
        Guid cardId,
        Guid authorId,
        string authorName,
        string content,
        string? authorAvatarUrl = null,
        List<string>? attachmentUrls = null,
        List<Guid>? mentionedUserIds = null)
    {
        var comment = new CardComment
        {
            Id = Guid.NewGuid(),
            CardId = cardId,
            AuthorId = authorId,
            AuthorName = authorName,
            AuthorAvatarUrl = authorAvatarUrl,
            Content = content,
            AttachmentUrls = attachmentUrls ?? [],
            MentionedUserIds = mentionedUserIds ?? [],
            IsEdited = false,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        comment.Apply(new CardCommentAddedEvent(comment.Id, cardId, authorId));
        return comment;
    }

    public void Update(string content)
    {
        Content = content;
        IsEdited = true;
        EditedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Apply(new CardCommentUpdatedEvent(Id, CardId));
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Apply(new CardCommentDeletedEvent(Id, CardId));
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}
