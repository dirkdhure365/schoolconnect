using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Calendar.Domain.Enums;
using SchoolConnect.Calendar.Domain.Events;

namespace SchoolConnect.Calendar.Domain.Entities;

public class TimetableChange : AggregateRoot
{
    public Guid TimetableSlotId { get; private set; }
    public Guid TimetableId { get; private set; }
    
    public DateTime OriginalDate { get; private set; }
    public ChangeType ChangeType { get; private set; }
    
    public Guid OriginalTeacherId { get; private set; }
    public string OriginalTeacherName { get; private set; } = string.Empty;
    public Guid? OriginalFacilityId { get; private set; }
    public string? OriginalFacilityName { get; private set; }
    
    public Guid? NewTeacherId { get; private set; }
    public string? NewTeacherName { get; private set; }
    public Guid? NewFacilityId { get; private set; }
    public string? NewFacilityName { get; private set; }
    public DateTime? NewDate { get; private set; }
    public Guid? NewPeriodId { get; private set; }
    
    public string Reason { get; private set; } = string.Empty;
    public string? Notes { get; private set; }
    
    public bool NotificationSent { get; private set; }
    public DateTime? NotificationSentAt { get; private set; }
    
    public new Guid CreatedBy { get; private set; }
    public string CreatedByName { get; private set; } = string.Empty;

    private TimetableChange() { }

    public static TimetableChange Create(
        Guid timetableSlotId,
        Guid timetableId,
        DateTime originalDate,
        ChangeType changeType,
        Guid originalTeacherId,
        string originalTeacherName,
        string reason,
        Guid createdBy,
        string createdByName,
        Guid? originalFacilityId = null,
        string? originalFacilityName = null,
        Guid? newTeacherId = null,
        string? newTeacherName = null,
        Guid? newFacilityId = null,
        string? newFacilityName = null,
        DateTime? newDate = null,
        Guid? newPeriodId = null,
        string? notes = null)
    {
        var change = new TimetableChange
        {
            Id = Guid.NewGuid(),
            TimetableSlotId = timetableSlotId,
            TimetableId = timetableId,
            OriginalDate = originalDate,
            ChangeType = changeType,
            OriginalTeacherId = originalTeacherId,
            OriginalTeacherName = originalTeacherName,
            OriginalFacilityId = originalFacilityId,
            OriginalFacilityName = originalFacilityName,
            NewTeacherId = newTeacherId,
            NewTeacherName = newTeacherName,
            NewFacilityId = newFacilityId,
            NewFacilityName = newFacilityName,
            NewDate = newDate,
            NewPeriodId = newPeriodId,
            Reason = reason,
            Notes = notes,
            NotificationSent = false,
            CreatedBy = createdBy,
            CreatedByName = createdByName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        if (changeType == ChangeType.Substitution && newTeacherId.HasValue)
        {
            change.Apply(new SubstitutionCreatedEvent(
                change.Id,
                timetableSlotId,
                newTeacherId.Value,
                reason));
        }

        return change;
    }

    public void MarkNotificationSent()
    {
        NotificationSent = true;
        NotificationSentAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Apply(new TimetableChangeNotifiedEvent(Id, Id));
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing handler - can be implemented if needed
    }
}
