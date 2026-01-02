using MediatR;
using SchoolConnect.Calendar.Application.Commands.Timetables;
using SchoolConnect.Calendar.Application.DTOs;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.Enums;
using SchoolConnect.Calendar.Domain.Exceptions;
using SchoolConnect.Calendar.Domain.Interfaces;

namespace SchoolConnect.Calendar.Application.Handlers;

public class CreateTimetableCommandHandler : IRequestHandler<CreateTimetableCommand, Guid>
{
    private readonly ITimetableRepository _timetableRepository;

    public CreateTimetableCommandHandler(ITimetableRepository timetableRepository)
    {
        _timetableRepository = timetableRepository;
    }

    public async Task<Guid> Handle(
        CreateTimetableCommand request,
        CancellationToken cancellationToken
    )
    {
        // Convert TimetableSettingsDto to TimetableSettings
        TimetableSettings? settings = request.Settings is not null
            ? new TimetableSettings
            {
                DayStartTime = request.Settings.DayStartTime,
                DayEndTime = request.Settings.DayEndTime,
                DefaultPeriodDurationMinutes = request.Settings.DefaultPeriodDurationMinutes,
                BreakDurationMinutes = request.Settings.BreakDurationMinutes,
                WorkingDays = request.Settings.WorkingDays,
                AllowDoubleBooking = request.Settings.AllowDoubleBooking,
                RequireFacility = request.Settings.RequireFacility
            }
            : null;

        var timetable = Timetable.Create(
            request.InstituteId,
            request.CentreId,
            request.Name,
            request.AcademicYear,
            request.EffectiveFrom,
            request.EffectiveTo,
            request.CreatedBy,
            request.Description,
            request.TermNumber,
            settings // Use the converted settings
        );

        await _timetableRepository.AddAsync(timetable, cancellationToken);
        return timetable.Id;
    }
}

public class UpdateTimetableCommandHandler : IRequestHandler<UpdateTimetableCommand, Unit>
{
    private readonly ITimetableRepository _timetableRepository;

    public UpdateTimetableCommandHandler(ITimetableRepository timetableRepository)
    {
        _timetableRepository = timetableRepository;
    }

    public async Task<Unit> Handle(
        UpdateTimetableCommand request,
        CancellationToken cancellationToken
    )
    {
        var timetable =
            await _timetableRepository.GetByIdAsync(request.TimetableId, cancellationToken)
            ?? throw new TimetableNotFoundException(request.TimetableId);

        timetable.Update(
            request.Name,
            request.Description,
            request.EffectiveFrom,
            request.EffectiveTo
        );

        await _timetableRepository.UpdateAsync(timetable, cancellationToken);
        return Unit.Value;
    }
}

public class PublishTimetableCommandHandler : IRequestHandler<PublishTimetableCommand, Unit>
{
    private readonly ITimetableRepository _timetableRepository;

    public PublishTimetableCommandHandler(ITimetableRepository timetableRepository)
    {
        _timetableRepository = timetableRepository;
    }

    public async Task<Unit> Handle(
        PublishTimetableCommand request,
        CancellationToken cancellationToken
    )
    {
        var timetable =
            await _timetableRepository.GetByIdAsync(request.TimetableId, cancellationToken)
            ?? throw new TimetableNotFoundException(request.TimetableId);

        timetable.Publish(request.PublishedBy);
        await _timetableRepository.UpdateAsync(timetable, cancellationToken);
        return Unit.Value;
    }
}

public class CreateSlotCommandHandler : IRequestHandler<CreateSlotCommand, TimetableSlotDto>
{
    private readonly ITimetableSlotRepository _slotRepository;

    public CreateSlotCommandHandler(ITimetableSlotRepository slotRepository)
    {
        _slotRepository = slotRepository;
    }

    public async Task<TimetableSlotDto> Handle(
        CreateSlotCommand request,
        CancellationToken cancellationToken
    )
    {
        var slot = TimetableSlot.Create(
            request.TimetableId,
            request.PeriodId,
            request.DayOfWeek,
            request.ClassId,
            request.ClassName,
            request.CohortId,
            request.CohortName,
            request.SubjectId,
            request.SubjectName,
            request.SubjectCode,
            request.TeacherId,
            request.TeacherName,
            request.FacilityId,
            request.FacilityName,
            request.Notes,
            request.Color
        );

        await _slotRepository.AddAsync(slot, cancellationToken);

        // Map to TimetableSlotDto (assuming a constructor or mapping exists)
        return new TimetableSlotDto
        {
            Id = slot.Id,
            TimetableId = slot.TimetableId,
            TimetablePeriodId = slot.TimetablePeriodId, // Use TimetablePeriodId instead of PeriodId
            DayOfWeek = slot.DayOfWeek,
            ClassId = slot.ClassId,
            ClassName = slot.ClassName,
            CohortId = slot.CohortId,
            CohortName = slot.CohortName,
            SubjectId = slot.SubjectId,
            SubjectName = slot.SubjectName,
            SubjectCode = slot.SubjectCode,
            TeacherId = slot.TeacherId,
            TeacherName = slot.TeacherName,
            FacilityId = slot.FacilityId,
            FacilityName = slot.FacilityName,
            Notes = slot.Notes,
            Color = slot.Color
        };
    }
}

public class CreateSubstitutionCommandHandler : IRequestHandler<CreateSubstitutionCommand, Guid>
{
    public async Task<Guid> Handle(
        CreateSubstitutionCommand request,
        CancellationToken cancellationToken
    )
    {
        var change = TimetableChange.Create(
            request.TimetableSlotId,
            request.TimetableId,
            request.Date,
            ChangeType.Substitution,
            request.OriginalTeacherId,
            request.OriginalTeacherName,
            request.Reason,
            request.CreatedBy,
            request.CreatedByName,
            newTeacherId: request.NewTeacherId,
            newTeacherName: request.NewTeacherName,
            notes: request.Notes
        );

        // In a real implementation, this would be saved via a repository
        return change.Id;
    }
}

public record CreateTimetableCommand
    : IRequest<Guid>,
        IBaseRequest,
        IEquatable<CreateTimetableCommand>
{
    public Guid InstituteId { get; init; }
    public Guid CentreId { get; init; }
    public string Name { get; init; }
    public int AcademicYear { get; init; }
    public DateTime EffectiveFrom { get; init; }
    public DateTime EffectiveTo { get; init; }
    public Guid CreatedBy { get; init; }
    public string? Description { get; init; }
    public int? TermNumber { get; init; }
    public TimetableSettingsDto? Settings { get; init; }
}
