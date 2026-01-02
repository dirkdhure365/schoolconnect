using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Collaboration.Domain.Enums;
using SchoolConnect.Collaboration.Domain.ValueObjects;
using SchoolConnect.Collaboration.Domain.Events;

namespace SchoolConnect.Collaboration.Domain.Entities;

public class Board : AggregateRoot
{
    public Guid WorkspaceId { get; private set; }
    public string WorkspaceName { get; private set; } = string.Empty;
    
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public BoardBackground Background { get; private set; } = BoardBackground.Create("Color", "#0079BF");
    
    public bool IsTemplate { get; private set; }
    public Guid? ClonedFromId { get; private set; }
    public bool IsStarred { get; private set; }
    
    public BoardStatus Status { get; private set; }
    public bool IsArchived { get; private set; }
    public DateTime? ArchivedAt { get; private set; }
    
    public int ListCount { get; private set; }
    public int CardCount { get; private set; }
    
    public new Guid CreatedBy { get; private set; }
    public DateTime? LastActivityAt { get; private set; }

    private Board() { }

    public static Board Create(
        Guid workspaceId,
        string workspaceName,
        string name,
        Guid createdBy,
        string? description = null,
        BoardBackground? background = null,
        bool isTemplate = false,
        Guid? clonedFromId = null)
    {
        var board = new Board
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspaceId,
            WorkspaceName = workspaceName,
            Name = name,
            Description = description,
            Background = background ?? BoardBackground.Create("Color", "#0079BF"),
            IsTemplate = isTemplate,
            ClonedFromId = clonedFromId,
            IsStarred = false,
            Status = BoardStatus.Active,
            IsArchived = false,
            ListCount = 0,
            CardCount = 0,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            LastActivityAt = DateTime.UtcNow
        };

        board.Apply(new BoardCreatedEvent(board.Id, workspaceId, name, createdBy));
        return board;
    }

    public void Update(
        string? name = null,
        string? description = null,
        BoardBackground? background = null)
    {
        if (name != null) Name = name;
        if (description != null) Description = description;
        if (background != null) Background = background;
        
        UpdatedAt = DateTime.UtcNow;
        LastActivityAt = DateTime.UtcNow;
        Apply(new BoardUpdatedEvent(Id, Name));
    }

    public void Archive()
    {
        IsArchived = true;
        ArchivedAt = DateTime.UtcNow;
        Status = BoardStatus.Archived;
        UpdatedAt = DateTime.UtcNow;
        Apply(new BoardArchivedEvent(Id));
    }

    public void Restore()
    {
        IsArchived = false;
        ArchivedAt = null;
        Status = BoardStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        Status = BoardStatus.Deleted;
        UpdatedAt = DateTime.UtcNow;
        Apply(new BoardDeletedEvent(Id, WorkspaceId));
    }

    public void ToggleStar()
    {
        IsStarred = !IsStarred;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementListCount()
    {
        ListCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DecrementListCount()
    {
        if (ListCount > 0) ListCount--;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementCardCount()
    {
        CardCount++;
        UpdatedAt = DateTime.UtcNow;
        LastActivityAt = DateTime.UtcNow;
    }

    public void DecrementCardCount()
    {
        if (CardCount > 0) CardCount--;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}
