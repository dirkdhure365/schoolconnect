namespace SchoolConnect.Collaboration.Application.DTOs;

public class CardCommentDto
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string? AuthorAvatarUrl { get; set; }
    public string Content { get; set; } = string.Empty;
    public List<string> AttachmentUrls { get; set; } = [];
    public List<Guid> MentionedUserIds { get; set; } = [];
    public bool IsEdited { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? EditedAt { get; set; }
}
