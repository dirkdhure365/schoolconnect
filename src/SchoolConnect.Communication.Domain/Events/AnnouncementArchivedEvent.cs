using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Communication.Domain.Events;

public record AnnouncementArchivedEvent(
    Guid AnnouncementId,
    Guid ArchivedBy) : DomainEvent
{
    public new Guid AggregateId { get; init; } = AnnouncementId;
    public new string AggregateType { get; init; } = nameof(Entities.Announcement);
}
