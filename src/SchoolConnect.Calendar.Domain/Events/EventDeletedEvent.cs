using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record EventDeletedEvent(
    Guid EventId,
    Guid DeletedBy) : DomainEvent
{
    public new Guid AggregateId { get; init; } = EventId;
    public new string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}
