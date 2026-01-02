using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Collaboration.Domain.Events;

namespace SchoolConnect.Collaboration.Domain.Entities;

public class CardLabel : AggregateRoot
{
    public Guid BoardId { get; private set; }
    
    public string Name { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;
    
    public int UsageCount { get; private set; }

    private CardLabel() { }

    public static CardLabel Create(
        Guid boardId,
        string name,
        string color)
    {
        var label = new CardLabel
        {
            Id = Guid.NewGuid(),
            BoardId = boardId,
            Name = name,
            Color = color,
            UsageCount = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return label;
    }

    public void Update(string? name = null, string? color = null)
    {
        if (name != null) Name = name;
        if (color != null) Color = color;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementUsage()
    {
        UsageCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DecrementUsage()
    {
        if (UsageCount > 0) UsageCount--;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}
