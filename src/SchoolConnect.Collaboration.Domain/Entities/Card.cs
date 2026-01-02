using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Collaboration.Domain.Enums;
using SchoolConnect.Collaboration.Domain.ValueObjects;
using SchoolConnect.Collaboration.Domain.Events;

namespace SchoolConnect.Collaboration.Domain.Entities;

public class Card : AggregateRoot
{
    public Guid ListId { get; private set; }
    public Guid BoardId { get; private set; }
    
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int Position { get; private set; }
    
    public List<Guid> AssigneeIds { get; private set; } = [];
    public List<AssigneeInfo> Assignees { get; private set; } = [];
    
    public List<Guid> LabelIds { get; private set; } = [];
    public DateTime? DueDate { get; private set; }
    public DateTime? StartDate { get; private set; }
    public bool IsDueComplete { get; private set; }
    public CardPriority? Priority { get; private set; }
    
    public CardCover? Cover { get; private set; }
    public List<CardAttachment> Attachments { get; private set; } = [];
    
    public int ChecklistCount { get; private set; }
    public int ChecklistItemsTotal { get; private set; }
    public int ChecklistItemsComplete { get; private set; }
    public decimal ChecklistProgress => ChecklistItemsTotal > 0 ? (decimal)ChecklistItemsComplete / ChecklistItemsTotal * 100 : 0;
    
    public int CommentCount { get; private set; }
    public int AttachmentCount { get; private set; }
    
    public List<Guid> WatcherIds { get; private set; } = [];
    
    public CardStatus Status { get; private set; }
    public bool IsArchived { get; private set; }
    public DateTime? ArchivedAt { get; private set; }
    
    public new Guid CreatedBy { get; private set; }
    public DateTime? LastActivityAt { get; private set; }

    private Card() { }

    public static Card Create(
        Guid listId,
        Guid boardId,
        string title,
        Guid createdBy,
        int position,
        string? description = null)
    {
        var card = new Card
        {
            Id = Guid.NewGuid(),
            ListId = listId,
            BoardId = boardId,
            Title = title,
            Description = description,
            Position = position,
            Status = CardStatus.Active,
            IsArchived = false,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            LastActivityAt = DateTime.UtcNow
        };

        card.Apply(new CardCreatedEvent(card.Id, listId, boardId, title, createdBy));
        return card;
    }

    public void Update(
        string? title = null,
        string? description = null,
        CardPriority? priority = null)
    {
        if (title != null) Title = title;
        if (description != null) Description = description;
        if (priority.HasValue) Priority = priority;
        
        UpdatedAt = DateTime.UtcNow;
        LastActivityAt = DateTime.UtcNow;
        Apply(new CardUpdatedEvent(Id, Title));
    }

    public void Move(Guid newListId, int newPosition)
    {
        var oldListId = ListId;
        ListId = newListId;
        Position = newPosition;
        UpdatedAt = DateTime.UtcNow;
        LastActivityAt = DateTime.UtcNow;
        Apply(new CardMovedEvent(Id, oldListId, newListId, newPosition));
    }

    public void Archive()
    {
        IsArchived = true;
        ArchivedAt = DateTime.UtcNow;
        Status = CardStatus.Archived;
        UpdatedAt = DateTime.UtcNow;
        Apply(new CardArchivedEvent(Id, BoardId));
    }

    public void Restore()
    {
        IsArchived = false;
        ArchivedAt = null;
        Status = CardStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        Status = CardStatus.Deleted;
        UpdatedAt = DateTime.UtcNow;
        Apply(new CardDeletedEvent(Id, ListId, BoardId));
    }

    public void AssignUser(Guid userId, string userName, string? avatarUrl = null)
    {
        if (!AssigneeIds.Contains(userId))
        {
            AssigneeIds.Add(userId);
            Assignees.Add(new AssigneeInfo { UserId = userId, Name = userName, AvatarUrl = avatarUrl });
            UpdatedAt = DateTime.UtcNow;
            LastActivityAt = DateTime.UtcNow;
            Apply(new CardAssignedEvent(Id, userId));
        }
    }

    public void UnassignUser(Guid userId)
    {
        AssigneeIds.Remove(userId);
        var assignee = Assignees.FirstOrDefault(a => a.UserId == userId);
        if (assignee != null) Assignees.Remove(assignee);
        UpdatedAt = DateTime.UtcNow;
        LastActivityAt = DateTime.UtcNow;
        Apply(new CardUnassignedEvent(Id, userId));
    }

    public void SetDueDate(DateTime? dueDate, DateTime? startDate = null)
    {
        DueDate = dueDate;
        if (startDate.HasValue) StartDate = startDate;
        IsDueComplete = false;
        UpdatedAt = DateTime.UtcNow;
        LastActivityAt = DateTime.UtcNow;
        if (dueDate.HasValue)
            Apply(new CardDueDateSetEvent(Id, dueDate.Value));
    }

    public void CompleteDueDate()
    {
        IsDueComplete = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddLabel(Guid labelId)
    {
        if (!LabelIds.Contains(labelId))
        {
            LabelIds.Add(labelId);
            UpdatedAt = DateTime.UtcNow;
            LastActivityAt = DateTime.UtcNow;
            Apply(new CardLabelAddedEvent(Id, labelId));
        }
    }

    public void RemoveLabel(Guid labelId)
    {
        LabelIds.Remove(labelId);
        UpdatedAt = DateTime.UtcNow;
        LastActivityAt = DateTime.UtcNow;
        Apply(new CardLabelRemovedEvent(Id, labelId));
    }

    public void SetCover(CardCover cover)
    {
        Cover = cover;
        UpdatedAt = DateTime.UtcNow;
        LastActivityAt = DateTime.UtcNow;
    }

    public void RemoveCover()
    {
        Cover = null;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddAttachment(CardAttachment attachment)
    {
        Attachments.Add(attachment);
        AttachmentCount = Attachments.Count;
        UpdatedAt = DateTime.UtcNow;
        LastActivityAt = DateTime.UtcNow;
    }

    public void RemoveAttachment(Guid attachmentId)
    {
        var attachment = Attachments.FirstOrDefault(a => a.Id == attachmentId);
        if (attachment != null)
        {
            Attachments.Remove(attachment);
            AttachmentCount = Attachments.Count;
            UpdatedAt = DateTime.UtcNow;
            LastActivityAt = DateTime.UtcNow;
        }
    }

    public void AddWatcher(Guid userId)
    {
        if (!WatcherIds.Contains(userId))
        {
            WatcherIds.Add(userId);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void RemoveWatcher(Guid userId)
    {
        WatcherIds.Remove(userId);
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementCommentCount()
    {
        CommentCount++;
        UpdatedAt = DateTime.UtcNow;
        LastActivityAt = DateTime.UtcNow;
    }

    public void DecrementCommentCount()
    {
        if (CommentCount > 0) CommentCount--;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateChecklistProgress(int totalItems, int completedItems)
    {
        ChecklistItemsTotal = totalItems;
        ChecklistItemsComplete = completedItems;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementChecklistCount()
    {
        ChecklistCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DecrementChecklistCount()
    {
        if (ChecklistCount > 0) ChecklistCount--;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}
