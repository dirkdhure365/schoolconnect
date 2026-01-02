using MediatR;
using SchoolConnect.Calendar.Domain.Enums;
using SchoolConnect.Calendar.Domain.ValueObjects;

namespace SchoolConnect.Calendar.Application.Commands.Events;

public record CreateEventCommand(
    Guid InstituteId,
    string Title,
    DateTime StartTime,
    DateTime EndTime,
    Guid CreatedBy,
    string CreatedByName,
    Guid? CentreId = null,
    Guid? ClassId = null,
    string? Description = null,
    EventLocation? Location = null,
    bool IsAllDay = false,
    string? Timezone = null,
    EventType Type = EventType.Other,
    EventVisibility Visibility = EventVisibility.Public,
    bool RsvpRequired = false
) : IRequest<Guid>;

public record UpdateEventCommand(
    Guid EventId,
    string? Title = null,
    string? Description = null,
    EventLocation? Location = null,
    DateTime? StartTime = null,
    DateTime? EndTime = null,
    bool? IsAllDay = null,
    EventType? Type = null,
    EventVisibility? Visibility = null
) : IRequest<Unit>;

public record CancelEventCommand(
    Guid EventId,
    string? CancellationReason = null
) : IRequest<Unit>;

public record DeleteEventCommand(
    Guid EventId
) : IRequest<Unit>;

public record RsvpEventCommand(
    Guid EventId,
    Guid UserId,
    RsvpStatus RsvpStatus,
    string? Notes = null
) : IRequest<Unit>;

public record AddAttendeeCommand(
    Guid EventId,
    Guid UserId,
    string UserName,
    Guid AddedBy,
    string? UserEmail = null,
    string? Role = null,
    bool IsOrganizer = false
) : IRequest<Guid>;

public record RemoveAttendeeCommand(
    Guid EventId,
    Guid UserId
) : IRequest<Unit>;
