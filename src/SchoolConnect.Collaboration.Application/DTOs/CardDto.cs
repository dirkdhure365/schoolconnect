using SchoolConnect.Collaboration.Domain.Enums;

namespace SchoolConnect.Collaboration.Application.DTOs;

public class CardDto
{
    public Guid Id { get; set; }
    public Guid ListId { get; set; }
    public Guid BoardId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Position { get; set; }
    public List<AssigneeInfoDto> Assignees { get; set; } = [];
    public List<Guid> LabelIds { get; set; } = [];
    public DateTime? DueDate { get; set; }
    public DateTime? StartDate { get; set; }
    public bool IsDueComplete { get; set; }
    public CardPriority? Priority { get; set; }
    public CardCoverDto? Cover { get; set; }
    public int ChecklistCount { get; set; }
    public int ChecklistItemsTotal { get; set; }
    public int ChecklistItemsComplete { get; set; }
    public decimal ChecklistProgress { get; set; }
    public int CommentCount { get; set; }
    public int AttachmentCount { get; set; }
    public CardStatus Status { get; set; }
    public bool IsArchived { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? LastActivityAt { get; set; }
}

public class CardSummaryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Position { get; set; }
    public List<Guid> LabelIds { get; set; } = [];
    public DateTime? DueDate { get; set; }
    public int CommentCount { get; set; }
}

public class AssigneeInfoDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
}

public class CardCoverDto
{
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Size { get; set; }
}

public class CardAttachmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? MimeType { get; set; }
    public long? SizeBytes { get; set; }
    public string? ThumbnailUrl { get; set; }
    public Guid UploadedBy { get; set; }
    public DateTime UploadedAt { get; set; }
}
