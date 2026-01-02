using SchoolConnect.Communication.Domain.Enums;

namespace SchoolConnect.Communication.Application.DTOs;

public class FeedItemDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid InstituteId { get; set; }
    public FeedItemType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string? ImageUrl { get; set; }
    public Guid SourceId { get; set; }
    public string SourceType { get; set; } = string.Empty;
    public string? ActionUrl { get; set; }
    public string? ActionText { get; set; }
    public int Priority { get; set; }
    public bool IsRead { get; set; }
    public bool IsDismissed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
}

public class FeedPreferencesDto
{
    public Guid UserId { get; set; }
    public List<FeedItemType> EnabledTypes { get; set; } = [];
    public bool ShowAnnouncements { get; set; }
    public bool ShowHomework { get; set; }
    public bool ShowGrades { get; set; }
}
