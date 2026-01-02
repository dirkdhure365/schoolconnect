using MediatR;
using SchoolConnect.Calendar.Application.DTOs;

namespace SchoolConnect.Calendar.Application.Queries.Timetables;

public record GetTimetableByIdQuery(
    Guid TimetableId
) : IRequest<TimetableDto?>;

public record GetTimetablesByInstituteQuery(
    Guid InstituteId
) : IRequest<IEnumerable<TimetableDto>>;

public record GetTimetablePeriodsQuery(
    Guid TimetableId
) : IRequest<IEnumerable<TimetablePeriodDto>>;

public record GetTimetableSlotsQuery(
    Guid TimetableId
) : IRequest<IEnumerable<TimetableSlotDto>>;

public record GetTeacherTimetableQuery(
    Guid TeacherId,
    Guid TimetableId
) : IRequest<IEnumerable<TimetableSlotDto>>;

public record GetClassTimetableQuery(
    Guid ClassId,
    Guid TimetableId
) : IRequest<IEnumerable<TimetableSlotDto>>;

public record GetTimetableChangesQuery(
    Guid TimetableId,
    DateTime? FromDate = null,
    DateTime? ToDate = null
) : IRequest<IEnumerable<TimetableChangeDto>>;
