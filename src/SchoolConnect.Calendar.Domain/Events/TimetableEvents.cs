using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record TimetableCreatedEvent(
    Guid AggregateId,
    Guid InstituteId,
    Guid CentreId,
    string Name,
    Guid CreatedBy
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.Timetable);
}

public record TimetableUpdatedEvent(
    Guid AggregateId,
    string Name
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.Timetable);
}

public record TimetablePublishedEvent(
    Guid AggregateId,
    Guid PublishedBy
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.Timetable);
}

public record TimetableSlotCreatedEvent(
    Guid AggregateId,
    Guid TimetableId,
    Guid ClassId,
    Guid SubjectId,
    Guid TeacherId
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.TimetableSlot);
}

public record TimetableSlotUpdatedEvent(
    Guid AggregateId
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.TimetableSlot);
}

public record TimetableSlotDeletedEvent(
    Guid AggregateId
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.TimetableSlot);
}

public record SubstitutionCreatedEvent(
    Guid AggregateId,
    Guid TimetableSlotId,
    Guid NewTeacherId,
    string Reason
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.TimetableChange);
}

public record TimetableConflictDetectedEvent(
    Guid ConflictId,
    string ConflictType,
    string Description
) : DomainEvent
{
    public string AggregateType { get; init; } = "TimetableConflict";
}

public record TimetableChangeNotifiedEvent(
    Guid AggregateId,
    Guid TimetableChangeId
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.TimetableChange);
}
