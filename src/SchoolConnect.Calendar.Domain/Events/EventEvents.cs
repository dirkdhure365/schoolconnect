using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.Events;

public record EventCreatedEvent(
    Guid AggregateId,
    Guid InstituteId,
    string Title,
    DateTime StartTime,
    DateTime EndTime,
    Guid CreatedBy
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}

public record EventUpdatedEvent(
    Guid AggregateId,
    string Title
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}

public record EventCancelledEvent(
    Guid AggregateId,
    string? CancellationReason
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}

public record EventDeletedEvent(
    Guid AggregateId
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}

public record EventRsvpEvent(
    Guid AggregateId,
    Guid UserId,
    string RsvpStatus
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}

public record EventReminderSentEvent(
    Guid AggregateId,
    Guid ReminderId,
    Guid UserId
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}

public record AttendeeAddedEvent(
    Guid AggregateId,
    Guid UserId,
    string UserName
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}

public record AttendeeRemovedEvent(
    Guid AggregateId,
    Guid UserId
) : DomainEvent
{
    public string AggregateType { get; init; } = nameof(Entities.CalendarEvent);
}
