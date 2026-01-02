using MediatR;
using SchoolConnect.Calendar.Application.DTOs;
using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Application.Commands.Timetables;

public record CreateSlotCommand(
    Guid TimetableId,
    Guid PeriodId,
    DayOfWeek DayOfWeek,
    Guid ClassId,
    string ClassName,
    Guid CohortId,
    string CohortName,
    Guid SubjectId,
    string SubjectName,
    string SubjectCode,
    Guid TeacherId,
    string TeacherName,
    Guid? FacilityId = null,
    string? FacilityName = null,
    string? Notes = null,
    string? Color = null) : IRequest<TimetableSlotDto>;
