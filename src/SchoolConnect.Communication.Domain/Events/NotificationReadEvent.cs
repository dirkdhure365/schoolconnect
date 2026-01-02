using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Communication.Domain.Events;

public record NotificationReadEvent(
    Guid NotificationId,
    Guid UserId) : DomainEvent
{
    public new Guid AggregateId { get; init; } = NotificationId;
    public new string AggregateType { get; init; } = nameof(Entities.Notification);
}
