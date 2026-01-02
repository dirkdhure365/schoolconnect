using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record AttendeeRemoved2Event(Guid EventId, Guid UserId, Guid RemovedBy) : DomainEvent
{
    public new Guid AggregateId { get; init; } = EventId;
    public new string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}
