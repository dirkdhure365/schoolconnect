using MediatR;
using SchoolConnect.Calendar.Application.DTOs;

namespace SchoolConnect.Calendar.Application.Commands.Timetables;

public record CreateTimetableCommand(
    Guid InstituteId,
    Guid CentreId,
    string Name,
    int AcademicYear,
    DateTime EffectiveFrom,
    DateTime EffectiveTo,
    Guid CreatedBy,
    string? Description = null,
    int? TermNumber = null,
    TimetableSettingsDto? Settings = null) : IRequest<TimetableDto>;
