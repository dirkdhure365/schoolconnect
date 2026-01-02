using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Communication.Domain.Events;

public record AnnouncementAcknowledgedEvent(
    Guid AnnouncementId,
    Guid UserId) : DomainEvent
{
    public new Guid AggregateId { get; init; } = AnnouncementId;
    public new string AggregateType { get; init; } = nameof(Entities.Announcement);
}
