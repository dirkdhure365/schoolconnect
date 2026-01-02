using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record TimetableSlotDeletedEvent(
    Guid SlotId,
    Guid TimetableId) : DomainEvent
{
    public new Guid AggregateId { get; init; } = SlotId;
    public new string AggregateType { get; init; } = nameof(Entities.TimetableSlot);
}
