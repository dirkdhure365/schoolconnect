using MediatR;
using SchoolConnect.Calendar.Application.DTOs;
using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Application.Commands.Events;

public record CreateEvent2Command(
    Guid InstituteId,
    string Title,
    DateTime StartTime,
    DateTime EndTime,
    EventType Type,
    EventVisibility Visibility,
    Guid CreatedBy,
    string CreatedByName,
    Guid? CentreId = null,
    Guid? ClassId = null,
    string? Description = null,
    EventLocationDto? Location = null,
    bool IsAllDay = false,
    string? Timezone = null,
    RecurrenceRuleDto? Recurrence = null,
    bool RsvpRequired = false,
    int? MaxAttendees = null,
    DateTime? RsvpDeadline = null,
    string? Color = null,
    string? IconUrl = null
) : IRequest<CalendarEventDto>;
