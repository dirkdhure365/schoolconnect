using SchoolConnect.Communication.Domain.Enums;

namespace SchoolConnect.Communication.Application.DTOs;

public class AnnouncementDto
{
    public Guid Id { get; set; }
    public Guid InstituteId { get; set; }
    public Guid? CentreId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public List<string> AttachmentUrls { get; set; } = [];
    public string? ImageUrl { get; set; }
    public TargetAudience TargetAudience { get; set; }
    public AnnouncementPriority Priority { get; set; }
    public bool IsPinned { get; set; }
    public DateTime? PinnedUntil { get; set; }
    public AnnouncementStatus Status { get; set; }
    public DateTime? ScheduledPublishAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public bool RequiresAcknowledgment { get; set; }
    public int AcknowledgmentCount { get; set; }
    public int TargetAudienceCount { get; set; }
    public int ViewCount { get; set; }
    public Guid CreatedBy { get; set; }
    public string CreatedByName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class AnnouncementSummaryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public AnnouncementPriority Priority { get; set; }
    public bool IsPinned { get; set; }
    public DateTime? PublishedAt { get; set; }
}

public class AcknowledgmentDto
{
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? UserRole { get; set; }
    public DateTime AcknowledgedAt { get; set; }
}

public class AnnouncementReachDto
{
    public int TotalTargeted { get; set; }
    public int Viewed { get; set; }
    public int Acknowledged { get; set; }
    public double ViewRate { get; set; }
    public double AcknowledgmentRate { get; set; }
}
