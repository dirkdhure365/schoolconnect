using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Collaboration.Domain.Events;

namespace SchoolConnect.Collaboration.Domain.Entities;

public class CardChecklist : AggregateRoot
{
    public Guid CardId { get; private set; }
    
    public string Title { get; private set; } = string.Empty;
    public int Position { get; private set; }
    
    public List<ChecklistItem> Items { get; private set; } = [];
    public int TotalItems => Items.Count;
    public int CompletedItems => Items.Count(i => i.IsCompleted);
    public decimal Progress => TotalItems > 0 ? (decimal)CompletedItems / TotalItems * 100 : 0;

    private CardChecklist() { }

    public static CardChecklist Create(
        Guid cardId,
        string title,
        int position)
    {
        var checklist = new CardChecklist
        {
            Id = Guid.NewGuid(),
            CardId = cardId,
            Title = title,
            Position = position,
            Items = [],
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        checklist.Apply(new ChecklistCreatedEvent(checklist.Id, cardId, title));
        return checklist;
    }

    public void Update(string? title = null, int? position = null)
    {
        if (title != null) Title = title;
        if (position.HasValue) Position = position.Value;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddItem(ChecklistItem item)
    {
        Items.Add(item);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveItem(Guid itemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            Items.Remove(item);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void ToggleItem(Guid itemId, Guid userId)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            item.Toggle(userId);
            UpdatedAt = DateTime.UtcNow;
            
            if (item.IsCompleted)
                Apply(new ChecklistItemCompletedEvent(Id, itemId, CardId, userId));
        }
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}
