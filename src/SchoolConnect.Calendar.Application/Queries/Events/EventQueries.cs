using MediatR;
using SchoolConnect.Calendar.Application.DTOs;

namespace SchoolConnect.Calendar.Application.Queries.Events;

public record GetEventByIdQuery(
    Guid EventId
) : IRequest<CalendarEventDto?>;

public record GetEventsQuery(
    Guid? InstituteId = null,
    Guid? CentreId = null,
    Guid? ClassId = null
) : IRequest<IEnumerable<CalendarEventDto>>;

public record GetEventsByDateRangeQuery(
    DateTime StartDate,
    DateTime EndDate,
    Guid? InstituteId = null
) : IRequest<IEnumerable<CalendarEventDto>>;

public record GetUpcomingEventsQuery(
    Guid UserId,
    int Count = 10
) : IRequest<IEnumerable<CalendarEventDto>>;

public record GetEventAttendeesQuery(
    Guid EventId
) : IRequest<IEnumerable<EventAttendeeDto>>;

public record GetEventRemindersQuery(
    Guid EventId
) : IRequest<IEnumerable<EventReminderDto>>;
