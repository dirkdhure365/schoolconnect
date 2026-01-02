using SchoolConnect.Calendar.Domain.Enums;
using SchoolConnect.Calendar.Domain.Events;
using SchoolConnect.Common.Domain.Primitives;

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
    public Guid? PublishedByUserId { get; private set; }

    public TimetableSettings Settings { get; private set; } = TimetableSettings.CreateDefault();

    public int PeriodCount { get; private set; }
    public int SlotCount { get; private set; }

    public Guid CreatedByUserId { get; private set; }

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
        TimetableSettings? settings = null
    )
    {
        if (effectiveTo <= effectiveFrom)
            throw new ArgumentException("Effective to date must be after effective from date");

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
            Settings = settings ?? TimetableSettings.CreateDefault(),
            Status = TimetableStatus.Draft,
            CreatedByUserId = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        timetable.Apply(
            new TimetableCreatedEvent(timetable.Id, instituteId, centreId, name, createdBy)
        );

        return timetable;
    }

    public void Update(
        string? name = null,
        string? description = null,
        DateTime? effectiveFrom = null,
        DateTime? effectiveTo = null,
        TimetableSettings? settings = null,
        Guid? updatedBy = null
    )
    {
        if (name != null)
            Name = name;
        if (description != null)
            Description = description;
        if (effectiveFrom.HasValue)
            EffectiveFrom = effectiveFrom.Value;
        if (effectiveTo.HasValue)
            EffectiveTo = effectiveTo.Value;
        if (settings != null)
            Settings = settings;

        UpdatedAt = DateTime.UtcNow;

        if (updatedBy.HasValue)
        {
            Apply(new TimetableUpdatedEvent(Id, Name) { AggregateType = nameof(Timetable) });
        }
    }

    public void Publish(Guid publishedBy)
    {
        Status = TimetableStatus.Published;
        PublishedAt = DateTime.UtcNow;
        PublishedByUserId = publishedBy;
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
        {
            PeriodCount--;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void IncrementSlotCount()
    {
        SlotCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DecrementSlotCount()
    {
        if (SlotCount > 0)
        {
            SlotCount--;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}
