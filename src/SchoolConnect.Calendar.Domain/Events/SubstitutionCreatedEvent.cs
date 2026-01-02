using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record SubstitutionCreated2Event(
    Guid ChangeId,
    Guid TimetableSlotId,
    Guid OriginalTeacherId,
    Guid NewTeacherId
) : DomainEvent
{
    public new Guid AggregateId { get; init; } = ChangeId;
    public new string AggregateType { get; init; } = nameof(Entities.TimetableChange);
}
