using MediatR;
using SchoolConnect.Calendar.Application.DTOs;

namespace SchoolConnect.Calendar.Application.Queries.Events;

public record GetEventsByDateRangeQuery(
    DateTime StartDate,
    DateTime EndDate,
    Guid? InstituteId = null,
    Guid? CentreId = null) : IRequest<IEnumerable<CalendarEventDto>>;
