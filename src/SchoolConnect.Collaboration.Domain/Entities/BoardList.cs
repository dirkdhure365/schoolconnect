using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Collaboration.Domain.Enums;
using SchoolConnect.Collaboration.Domain.Events;

namespace SchoolConnect.Collaboration.Domain.Entities;

public class BoardList : AggregateRoot
{
    public Guid BoardId { get; private set; }
    
    public string Name { get; private set; } = string.Empty;
    public string? Color { get; private set; }
    public int Position { get; private set; }
    
    public int? WipLimit { get; private set; } // Work-in-progress limit
    public int CardCount { get; private set; }
    
    public ListStatus Status { get; private set; }
    public bool IsArchived { get; private set; }
    public DateTime? ArchivedAt { get; private set; }

    private BoardList() { }

    public static BoardList Create(
        Guid boardId,
        string name,
        int position,
        string? color = null,
        int? wipLimit = null)
    {
        var list = new BoardList
        {
            Id = Guid.NewGuid(),
            BoardId = boardId,
            Name = name,
            Color = color,
            Position = position,
            WipLimit = wipLimit,
            CardCount = 0,
            Status = ListStatus.Active,
            IsArchived = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        list.Apply(new ListCreatedEvent(list.Id, boardId, name, position));
        return list;
    }

    public void Update(
        string? name = null,
        string? color = null,
        int? wipLimit = null)
    {
        if (name != null) Name = name;
        if (color != null) Color = color;
        if (wipLimit.HasValue) WipLimit = wipLimit;
        
        UpdatedAt = DateTime.UtcNow;
        Apply(new ListUpdatedEvent(Id, Name));
    }

    public void Move(int newPosition)
    {
        Position = newPosition;
        UpdatedAt = DateTime.UtcNow;
        Apply(new ListMovedEvent(Id, BoardId, newPosition));
    }

    public void Archive()
    {
        IsArchived = true;
        ArchivedAt = DateTime.UtcNow;
        Status = ListStatus.Archived;
        UpdatedAt = DateTime.UtcNow;
        Apply(new ListArchivedEvent(Id, BoardId));
    }

    public void Restore()
    {
        IsArchived = false;
        ArchivedAt = null;
        Status = ListStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementCardCount()
    {
        CardCount++;
        UpdatedAt = DateTime.UtcNow;
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
