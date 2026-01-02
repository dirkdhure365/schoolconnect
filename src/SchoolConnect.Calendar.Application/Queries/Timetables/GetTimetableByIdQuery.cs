using MediatR;
using SchoolConnect.Calendar.Application.DTOs;

namespace SchoolConnect.Calendar.Application.Queries.Timetables;

public record GetTimetableByIdQuery(Guid TimetableId) : IRequest<TimetableDto?>;
