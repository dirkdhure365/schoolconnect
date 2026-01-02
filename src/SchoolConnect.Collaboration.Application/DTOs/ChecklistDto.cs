namespace SchoolConnect.Collaboration.Application.DTOs;

public class ChecklistDto
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Position { get; set; }
    public List<ChecklistItemDto> Items { get; set; } = [];
    public int TotalItems { get; set; }
    public int CompletedItems { get; set; }
    public decimal Progress { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class ChecklistItemDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int Position { get; set; }
    public bool IsCompleted { get; set; }
    public Guid? AssigneeId { get; set; }
    public string? AssigneeName { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid? CompletedBy { get; set; }
    public DateTime? CompletedAt { get; set; }
}
