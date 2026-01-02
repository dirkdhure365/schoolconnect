using MediatR;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Application.Commands.Timetables;

public record CreateTimetable2Command(
    Guid InstituteId,
    Guid CentreId,
    string Name,
    int AcademicYear,
    DateTime EffectiveFrom,
    DateTime EffectiveTo,
    Guid CreatedBy,
    string? Description = null,
    int? TermNumber = null,
    TimetableSettings? Settings = null
) : IRequest<Guid>;

public record UpdateTimetableCommand(
    Guid TimetableId,
    string? Name = null,
    string? Description = null,
    DateTime? EffectiveFrom = null,
    DateTime? EffectiveTo = null
) : IRequest<Unit>;

public record DeleteTimetableCommand(Guid TimetableId) : IRequest<Unit>;

public record PublishTimetable2Command(Guid TimetableId, Guid PublishedBy) : IRequest<Unit>;

public record CreatePeriodCommand(
    Guid TimetableId,
    string Name,
    int PeriodNumber,
    TimeOnly StartTime,
    TimeOnly EndTime,
    PeriodType Type = PeriodType.Lesson,
    List<DayOfWeek>? ApplicableDays = null,
    string? Color = null
) : IRequest<Guid>;

public record UpdatePeriodCommand(
    Guid PeriodId,
    string? Name = null,
    TimeOnly? StartTime = null,
    TimeOnly? EndTime = null,
    PeriodType? Type = null
) : IRequest<Unit>;

public record DeletePeriodCommand(Guid PeriodId) : IRequest<Unit>;

public record CreateSlot2Command(
    Guid TimetableId,
    Guid TimetablePeriodId,
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
    string? Color = null
) : IRequest<Guid>;

public record UpdateSlotCommand(
    Guid SlotId,
    Guid? TeacherId = null,
    string? TeacherName = null,
    Guid? FacilityId = null,
    string? FacilityName = null,
    string? Notes = null
) : IRequest<Unit>;

public record DeleteSlotCommand(Guid SlotId) : IRequest<Unit>;

public record CreateSubstitutionCommand(
    Guid TimetableSlotId,
    Guid TimetableId,
    DateTime Date,
    Guid OriginalTeacherId,
    string OriginalTeacherName,
    Guid NewTeacherId,
    string NewTeacherName,
    string Reason,
    Guid CreatedBy,
    string CreatedByName,
    string? Notes = null
) : IRequest<Guid>;
