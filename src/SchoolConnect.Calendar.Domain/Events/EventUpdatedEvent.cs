using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record EventUpdatedEvent(
    Guid EventId,
    Guid UpdatedBy) : DomainEvent
{
    public new Guid AggregateId { get; init; } = EventId;
    public new string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}
