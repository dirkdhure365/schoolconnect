using MediatR;
using SchoolConnect.Calendar.Application.DTOs;

namespace SchoolConnect.Calendar.Application.Queries.Timetables;

public record GetTimetableById2Query(Guid TimetableId) : IRequest<TimetableDto?>;
