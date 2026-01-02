using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Domain.Events;

public record EventRsvpEvent(
    Guid EventId,
    Guid UserId,
    RsvpStatus Status) : DomainEvent
{
    public new Guid AggregateId { get; init; } = EventId;
    public new string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}
