using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record EventReminderSentEvent(
    Guid ReminderId,
    Guid EventId,
    Guid UserId) : DomainEvent
{
    public new Guid AggregateId { get; init; } = EventId;
    public new string AggregateType { get; init; } = nameof(Entities.EventReminder);
}
