using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Communication.Domain.Events;

public record AnnouncementCreatedEvent(
    Guid AnnouncementId,
    Guid InstituteId,
    string Title,
    Guid CreatedBy) : DomainEvent
{
    public new Guid AggregateId { get; init; } = AnnouncementId;
    public new string AggregateType { get; init; } = nameof(Entities.Announcement);
}
