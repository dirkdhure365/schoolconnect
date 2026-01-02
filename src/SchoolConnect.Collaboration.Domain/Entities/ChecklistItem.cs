using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Collaboration.Domain.Entities;

public class ChecklistItem : Entity
{
    public Guid ChecklistId { get; private set; }
    
    public string Text { get; private set; } = string.Empty;
    public int Position { get; private set; }
    public bool IsCompleted { get; private set; }
    
    public Guid? AssigneeId { get; private set; }
    public string? AssigneeName { get; private set; }
    public DateTime? DueDate { get; private set; }
    
    public Guid? CompletedBy { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private ChecklistItem() { }

    public static ChecklistItem Create(
        Guid checklistId,
        string text,
        int position,
        Guid? assigneeId = null,
        string? assigneeName = null,
        DateTime? dueDate = null)
    {
        return new ChecklistItem
        {
            Id = Guid.NewGuid(),
            ChecklistId = checklistId,
            Text = text,
            Position = position,
            IsCompleted = false,
            AssigneeId = assigneeId,
            AssigneeName = assigneeName,
            DueDate = dueDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? text = null,
        int? position = null,
        Guid? assigneeId = null,
        string? assigneeName = null,
        DateTime? dueDate = null)
    {
        if (text != null) Text = text;
        if (position.HasValue) Position = position.Value;
        if (assigneeId.HasValue) AssigneeId = assigneeId;
        if (assigneeName != null) AssigneeName = assigneeName;
        if (dueDate.HasValue) DueDate = dueDate;
        
        UpdatedAt = DateTime.UtcNow;
    }

    public void Toggle(Guid userId)
    {
        IsCompleted = !IsCompleted;
        
        if (IsCompleted)
        {
            CompletedBy = userId;
            CompletedAt = DateTime.UtcNow;
        }
        else
        {
            CompletedBy = null;
            CompletedAt = null;
        }
        
        UpdatedAt = DateTime.UtcNow;
    }
}
