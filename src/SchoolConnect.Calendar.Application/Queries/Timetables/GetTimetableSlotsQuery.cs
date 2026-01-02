using MediatR;
using SchoolConnect.Calendar.Application.DTOs;

namespace SchoolConnect.Calendar.Application.Queries.Timetables;

public record GetTimetableSlots2Query(Guid TimetableId, DayOfWeek? DayOfWeek = null)
    : IRequest<IEnumerable<TimetableSlotDto>>;
