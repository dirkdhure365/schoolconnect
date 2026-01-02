using MediatR;
using SchoolConnect.Calendar.Application.DTOs;

namespace SchoolConnect.Calendar.Application.Queries.Events;

public record GetEventByIdQuery(Guid EventId) : IRequest<CalendarEventDto?>;
