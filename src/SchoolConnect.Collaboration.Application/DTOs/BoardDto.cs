using SchoolConnect.Collaboration.Domain.Enums;

namespace SchoolConnect.Collaboration.Application.DTOs;

public class BoardDto
{
    public Guid Id { get; set; }
    public Guid WorkspaceId { get; set; }
    public string WorkspaceName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public BoardBackgroundDto Background { get; set; } = new();
    public bool IsTemplate { get; set; }
    public bool IsStarred { get; set; }
    public BoardStatus Status { get; set; }
    public bool IsArchived { get; set; }
    public int ListCount { get; set; }
    public int CardCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? LastActivityAt { get; set; }
}

public class BoardSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public BoardBackgroundDto Background { get; set; } = new();
    public bool IsStarred { get; set; }
    public int CardCount { get; set; }
}

public class BoardBackgroundDto
{
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
}
