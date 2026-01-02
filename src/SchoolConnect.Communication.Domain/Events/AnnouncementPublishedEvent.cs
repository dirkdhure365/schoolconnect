using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Communication.Domain.Enums;

namespace SchoolConnect.Communication.Domain.Events;

public record AnnouncementPublishedEvent(
    Guid AnnouncementId,
    Guid InstituteId,
    TargetAudience TargetAudience,
    int TargetCount) : DomainEvent
{
    public new Guid AggregateId { get; init; } = AnnouncementId;
    public new string AggregateType { get; init; } = nameof(Entities.Announcement);
}
