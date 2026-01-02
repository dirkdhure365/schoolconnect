using SchoolConnect.Calendar.Domain.Enums;
using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record EventRsvp2Event(Guid EventId, Guid UserId, RsvpStatus Status) : DomainEvent
{
    public new Guid AggregateId { get; init; } = EventId;
    public new string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}
