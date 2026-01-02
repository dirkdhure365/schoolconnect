using SchoolConnect.Collaboration.Domain.Enums;

namespace SchoolConnect.Collaboration.Application.DTOs;

public class ListDto
{
    public Guid Id { get; set; }
    public Guid BoardId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Color { get; set; }
    public int Position { get; set; }
    public int? WipLimit { get; set; }
    public int CardCount { get; set; }
    public ListStatus Status { get; set; }
    public bool IsArchived { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
