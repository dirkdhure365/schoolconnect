using MediatR;
using SchoolConnect.Calendar.Application.DTOs;

namespace SchoolConnect.Calendar.Application.Commands.Events;

public record UpdateEvent2Command(
    Guid EventId,
    Guid UpdatedBy,
    string? Title = null,
    string? Description = null,
    EventLocationDto? Location = null,
    DateTime? StartTime = null,
    DateTime? EndTime = null,
    bool? IsAllDay = null
) : IRequest<CalendarEventDto>;
