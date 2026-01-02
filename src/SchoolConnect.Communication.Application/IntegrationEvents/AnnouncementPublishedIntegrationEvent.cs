namespace SchoolConnect.Communication.Application.IntegrationEvents;

public record AnnouncementPublishedIntegrationEvent(
    Guid AnnouncementId,
    Guid InstituteId,
    string Title,
    string Content,
    List<Guid> TargetUserIds,
    DateTime PublishedAt
);
