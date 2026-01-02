using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Domain.Events;

public record TimetableConflictDetectedEvent(
    Guid TimetableId,
    ConflictType ConflictType,
    List<Guid> ConflictingSlotIds) : DomainEvent
{
    public new Guid AggregateId { get; init; } = TimetableId;
    public new string AggregateType { get; init; } = nameof(Entities.Timetable);
}
