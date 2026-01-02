using MediatR;
using SchoolConnect.Communication.Domain.Enums;

namespace SchoolConnect.Communication.Application.Commands.Announcements;

public record CreateAnnouncementCommand(
    Guid InstituteId,
    string Title,
    string Content,
    TargetAudience TargetAudience,
    Guid CreatedBy,
    string CreatedByName,
    Guid? CentreId = null,
    string? Summary = null,
    List<string>? AttachmentUrls = null,
    string? ImageUrl = null,
    AnnouncementPriority Priority = AnnouncementPriority.Normal,
    bool RequiresAcknowledgment = false,
    DateTime? ScheduledPublishAt = null,
    DateTime? ExpiresAt = null
) : IRequest<Guid>;
