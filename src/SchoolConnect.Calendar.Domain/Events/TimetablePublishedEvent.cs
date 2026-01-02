using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record TimetablePublished2Event(Guid TimetableId, Guid PublishedBy) : DomainEvent
{
    public new Guid AggregateId { get; init; } = TimetableId;
    public new string AggregateType { get; init; } = nameof(Entities.Timetable);
}
