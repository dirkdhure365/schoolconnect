using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Calendar.Domain.Enums;
using SchoolConnect.Calendar.Domain.Events;

namespace SchoolConnect.Calendar.Domain.Entities;

public class Timetable : AggregateRoot
{
    public Guid InstituteId { get; private set; }
    public Guid CentreId { get; private set; }
    
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int AcademicYear { get; private set; }
    public int? TermNumber { get; private set; }
    
    public DateTime EffectiveFrom { get; private set; }
    public DateTime EffectiveTo { get; private set; }
    
    public TimetableStatus Status { get; private set; }
    public DateTime? PublishedAt { get; private set; }
    public Guid? PublishedBy { get; private set; }
    
    public TimetableSettings Settings { get; private set; } = new();
    
    public int PeriodCount { get; private set; }
    public int SlotCount { get; private set; }
    
    public new Guid CreatedBy { get; private set; }

    private Timetable() { }

    public static Timetable Create(
        Guid instituteId,
        Guid centreId,
        string name,
        int academicYear,
        DateTime effectiveFrom,
        DateTime effectiveTo,
        Guid createdBy,
        string? description = null,
        int? termNumber = null,
        TimetableSettings? settings = null)
    {
        var timetable = new Timetable
        {
            Id = Guid.NewGuid(),
            InstituteId = instituteId,
            CentreId = centreId,
            Name = name,
            Description = description,
            AcademicYear = academicYear,
            TermNumber = termNumber,
            EffectiveFrom = effectiveFrom,
            EffectiveTo = effectiveTo,
            Status = TimetableStatus.Draft,
            Settings = settings ?? new TimetableSettings
            {
                DayStartTime = new TimeOnly(8, 0),
                DayEndTime = new TimeOnly(15, 0),
                DefaultPeriodDurationMinutes = 45,
                BreakDurationMinutes = 15,
                WorkingDays = [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday],
                AllowDoubleBooking = false,
                RequireFacility = true
            },
            PeriodCount = 0,
            SlotCount = 0,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        timetable.Apply(new TimetableCreatedEvent(
            timetable.Id,
            instituteId,
            centreId,
            name,
            createdBy));

        return timetable;
    }

    public void Update(
        string? name = null,
        string? description = null,
        DateTime? effectiveFrom = null,
        DateTime? effectiveTo = null,
        TimetableSettings? settings = null)
    {
        if (name != null) Name = name;
        if (description != null) Description = description;
        if (effectiveFrom != null) EffectiveFrom = effectiveFrom.Value;
        if (effectiveTo != null) EffectiveTo = effectiveTo.Value;
        if (settings != null) Settings = settings;

        UpdatedAt = DateTime.UtcNow;
        Apply(new TimetableUpdatedEvent(Id, Name));
    }

    public void Publish(Guid publishedBy)
    {
        Status = TimetableStatus.Published;
        PublishedAt = DateTime.UtcNow;
        PublishedBy = publishedBy;
        UpdatedAt = DateTime.UtcNow;
        Apply(new TimetablePublishedEvent(Id, publishedBy));
    }

    public void Archive()
    {
        Status = TimetableStatus.Archived;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementPeriodCount()
    {
        PeriodCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DecrementPeriodCount()
    {
        if (PeriodCount > 0)
            PeriodCount--;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementSlotCount()
    {
        SlotCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DecrementSlotCount()
    {
        if (SlotCount > 0)
            SlotCount--;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing handler - can be implemented if needed
    }
}
