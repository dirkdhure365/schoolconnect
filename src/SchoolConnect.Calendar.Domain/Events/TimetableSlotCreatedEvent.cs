using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record TimetableSlotCreatedEvent(
    Guid SlotId,
    Guid TimetableId,
    Guid ClassId,
    Guid TeacherId) : DomainEvent
{
    public new Guid AggregateId { get; init; } = SlotId;
    public new string AggregateType { get; init; } = nameof(Entities.TimetableSlot);
}
