using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record TimetableCreated2Event(
    Guid TimetableId,
    Guid InstituteId,
    Guid CentreId,
    string Name,
    Guid CreatedBy
) : DomainEvent
{
    public new Guid AggregateId { get; init; } = TimetableId;
    public new string AggregateType { get; init; } = nameof(Entities.Timetable);
}
