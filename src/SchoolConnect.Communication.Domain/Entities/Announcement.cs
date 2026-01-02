using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Communication.Domain.Enums;
using SchoolConnect.Communication.Domain.Events;
using SchoolConnect.Communication.Domain.ValueObjects;

namespace SchoolConnect.Communication.Domain.Entities;

public class Announcement : AggregateRoot
{
    public Guid InstituteId { get; private set; }
    public Guid? CentreId { get; private set; }
    
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public string? Summary { get; private set; }
    public List<string> AttachmentUrls { get; private set; } = [];
    public string? ImageUrl { get; private set; }
    
    public TargetAudience TargetAudience { get; private set; }
    public AudienceFilter? AudienceFilter { get; private set; }
    
    public AnnouncementPriority Priority { get; private set; }
    public bool IsPinned { get; private set; }
    public DateTime? PinnedUntil { get; private set; }
    
    public AnnouncementStatus Status { get; private set; }
    public DateTime? ScheduledPublishAt { get; private set; }
    public DateTime? PublishedAt { get; private set; }
    public DateTime? ExpiresAt { get; private set; }
    
    public bool RequiresAcknowledgment { get; private set; }
    public int AcknowledgmentCount { get; private set; }
    public int TargetAudienceCount { get; private set; }
    public int ViewCount { get; private set; }
    
    public Guid CreatedBy { get; private set; }
    public string CreatedByName { get; private set; } = string.Empty;

    private Announcement() { }

    public static Announcement Create(
        Guid instituteId,
        string title,
        string content,
        TargetAudience targetAudience,
        Guid createdBy,
        string createdByName,
        Guid? centreId = null,
        string? summary = null,
        List<string>? attachmentUrls = null,
        string? imageUrl = null,
        AudienceFilter? audienceFilter = null,
        AnnouncementPriority priority = AnnouncementPriority.Normal,
        bool requiresAcknowledgment = false,
        DateTime? scheduledPublishAt = null,
        DateTime? expiresAt = null)
    {
        var announcement = new Announcement
        {
            Id = Guid.NewGuid(),
            InstituteId = instituteId,
            CentreId = centreId,
            Title = title,
            Content = content,
            Summary = summary,
            AttachmentUrls = attachmentUrls ?? [],
            ImageUrl = imageUrl,
            TargetAudience = targetAudience,
            AudienceFilter = audienceFilter,
            Priority = priority,
            Status = AnnouncementStatus.Draft,
            ScheduledPublishAt = scheduledPublishAt,
            ExpiresAt = expiresAt,
            RequiresAcknowledgment = requiresAcknowledgment,
            AcknowledgmentCount = 0,
            TargetAudienceCount = 0,
            ViewCount = 0,
            IsPinned = false,
            CreatedBy = createdBy,
            CreatedByName = createdByName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        announcement.Apply(new AnnouncementCreatedEvent(announcement.Id, instituteId, title, createdBy));
        return announcement;
    }

    public void Update(
        string? title = null,
        string? content = null,
        string? summary = null,
        List<string>? attachmentUrls = null,
        string? imageUrl = null,
        TargetAudience? targetAudience = null,
        AudienceFilter? audienceFilter = null,
        AnnouncementPriority? priority = null,
        DateTime? expiresAt = null)
    {
        if (title != null) Title = title;
        if (content != null) Content = content;
        if (summary != null) Summary = summary;
        if (attachmentUrls != null) AttachmentUrls = attachmentUrls;
        if (imageUrl != null) ImageUrl = imageUrl;
        if (targetAudience.HasValue) TargetAudience = targetAudience.Value;
        if (audienceFilter != null) AudienceFilter = audienceFilter;
        if (priority.HasValue) Priority = priority.Value;
        if (expiresAt.HasValue) ExpiresAt = expiresAt;
        
        UpdatedAt = DateTime.UtcNow;
    }

    public void Publish(int targetCount)
    {
        Status = AnnouncementStatus.Published;
        PublishedAt = DateTime.UtcNow;
        TargetAudienceCount = targetCount;
        UpdatedAt = DateTime.UtcNow;
        Apply(new AnnouncementPublishedEvent(Id, InstituteId, TargetAudience, targetCount));
    }

    public void Schedule(DateTime publishAt)
    {
        Status = AnnouncementStatus.Scheduled;
        ScheduledPublishAt = publishAt;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Pin(DateTime? until = null)
    {
        IsPinned = true;
        PinnedUntil = until;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Unpin()
    {
        IsPinned = false;
        PinnedUntil = null;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        Status = AnnouncementStatus.Archived;
        UpdatedAt = DateTime.UtcNow;
        Apply(new AnnouncementArchivedEvent(Id, CreatedBy));
    }

    public void IncrementAcknowledgmentCount()
    {
        AcknowledgmentCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementViewCount()
    {
        ViewCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}
