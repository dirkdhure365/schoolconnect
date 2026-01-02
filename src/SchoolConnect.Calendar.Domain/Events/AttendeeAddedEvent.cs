using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record AttendeeAdded2Event(Guid EventId, Guid UserId, Guid AddedBy) : DomainEvent
{
    public new Guid AggregateId { get; init; } = EventId;
    public new string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}
