using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Communication.Domain.Events;

public record NotificationPreferenceUpdatedEvent(
    Guid PreferenceId,
    Guid UserId) : DomainEvent
{
    public new Guid AggregateId { get; init; } = PreferenceId;
    public new string AggregateType { get; init; } = nameof(Entities.NotificationPreference);
}
