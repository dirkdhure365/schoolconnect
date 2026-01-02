using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record TimetableChangeNotified2Event(
    Guid ChangeId,
    Guid TimetableSlotId,
    List<Guid> NotifiedUserIds
) : DomainEvent
{
    public new Guid AggregateId { get; init; } = ChangeId;
    public new string AggregateType { get; init; } = nameof(Entities.TimetableChange);
}
