using SchoolConnect.Calendar.Domain.Enums;
using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record TimetableConflictDetected2Event(
    Guid TimetableId,
    ConflictType ConflictType,
    List<Guid> ConflictingSlotIds
) : DomainEvent
{
    public new Guid AggregateId { get; init; } = TimetableId;
    public new string AggregateType { get; init; } = nameof(Entities.Timetable);
}
