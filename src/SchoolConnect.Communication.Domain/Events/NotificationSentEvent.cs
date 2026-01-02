using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Communication.Domain.Enums;

namespace SchoolConnect.Communication.Domain.Events;

public record NotificationSentEvent(
    Guid NotificationId,
    Guid UserId,
    NotificationType Type,
    List<NotificationChannel> Channels) : DomainEvent
{
    public new Guid AggregateId { get; init; } = NotificationId;
    public new string AggregateType { get; init; } = nameof(Entities.Notification);
}
