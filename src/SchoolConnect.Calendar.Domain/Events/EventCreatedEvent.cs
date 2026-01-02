using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record EventCreated2Event(
    Guid EventId,
    Guid InstituteId,
    string Title,
    DateTime StartTime,
    DateTime EndTime,
    Guid CreatedBy
) : DomainEvent
{
    public new Guid AggregateId { get; init; } = EventId;
    public new string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}
