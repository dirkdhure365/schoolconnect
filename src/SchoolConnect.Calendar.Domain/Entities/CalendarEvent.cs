using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Calendar.Domain.Enums;
using SchoolConnect.Calendar.Domain.ValueObjects;
using SchoolConnect.Calendar.Domain.Events;

namespace SchoolConnect.Calendar.Domain.Entities;

public class CalendarEvent : AggregateRoot
{
    public Guid InstituteId { get; private set; }
    public Guid? CentreId { get; private set; }
    public Guid? ClassId { get; private set; }
    
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public EventLocation? Location { get; private set; }
    
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public bool IsAllDay { get; private set; }
    public string? Timezone { get; private set; }
    
    public RecurrenceRule? Recurrence { get; private set; }
    public Guid? RecurrenceGroupId { get; private set; }
    public bool IsRecurrenceException { get; private set; }
    public DateTime? OriginalStartTime { get; private set; }
    
    public EventType Type { get; private set; }
    public string? Color { get; private set; }
    public string? IconUrl { get; private set; }
    
    public EventVisibility Visibility { get; private set; }
    public bool RsvpRequired { get; private set; }
    public int? MaxAttendees { get; private set; }
    public DateTime? RsvpDeadline { get; private set; }
    
    public int AttendeeCount { get; private set; }
    public int ConfirmedCount { get; private set; }
    public int DeclinedCount { get; private set; }
    
    public List<string> AttachmentUrls { get; private set; } = [];
    public Dictionary<string, string> CustomFields { get; private set; } = new();
    
    public EventStatus Status { get; private set; }
    public string? CancellationReason { get; private set; }
    
    public new Guid CreatedBy { get; private set; }
    public string CreatedByName { get; private set; } = string.Empty;

    private CalendarEvent() { }

    public static CalendarEvent Create(
        Guid instituteId,
        string title,
        DateTime startTime,
        DateTime endTime,
        Guid createdBy,
        string createdByName,
        Guid? centreId = null,
        Guid? classId = null,
        string? description = null,
        EventLocation? location = null,
        bool isAllDay = false,
        string? timezone = null,
        EventType type = EventType.Other,
        EventVisibility visibility = EventVisibility.Public,
        bool rsvpRequired = false)
    {
        var calendarEvent = new CalendarEvent
        {
            Id = Guid.NewGuid(),
            InstituteId = instituteId,
            CentreId = centreId,
            ClassId = classId,
            Title = title,
            Description = description,
            Location = location,
            StartTime = startTime,
            EndTime = endTime,
            IsAllDay = isAllDay,
            Timezone = timezone,
            Type = type,
            Visibility = visibility,
            RsvpRequired = rsvpRequired,
            Status = EventStatus.Scheduled,
            CreatedBy = createdBy,
            CreatedByName = createdByName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            AttendeeCount = 0,
            ConfirmedCount = 0,
            DeclinedCount = 0
        };

        calendarEvent.Apply(new EventCreatedEvent(
            calendarEvent.Id,
            instituteId,
            title,
            startTime,
            endTime,
            createdBy));

        return calendarEvent;
    }

    public void Update(
        string? title = null,
        string? description = null,
        EventLocation? location = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        bool? isAllDay = null,
        EventType? type = null,
        EventVisibility? visibility = null)
    {
        if (title != null) Title = title;
        if (description != null) Description = description;
        if (location != null) Location = location;
        if (startTime != null) StartTime = startTime.Value;
        if (endTime != null) EndTime = endTime.Value;
        if (isAllDay != null) IsAllDay = isAllDay.Value;
        if (type != null) Type = type.Value;
        if (visibility != null) Visibility = visibility.Value;

        UpdatedAt = DateTime.UtcNow;
        Apply(new EventUpdatedEvent(Id, Title));
    }

    public void Cancel(string? reason = null)
    {
        Status = EventStatus.Cancelled;
        CancellationReason = reason;
        UpdatedAt = DateTime.UtcNow;
        Apply(new EventCancelledEvent(Id, reason));
    }

    public void SetRecurrence(RecurrenceRule recurrence, Guid? recurrenceGroupId = null)
    {
        Recurrence = recurrence;
        RecurrenceGroupId = recurrenceGroupId ?? Guid.NewGuid();
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddAttendee()
    {
        AttendeeCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveAttendee()
    {
        if (AttendeeCount > 0)
            AttendeeCount--;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateRsvpCounts(RsvpStatus oldStatus, RsvpStatus newStatus)
    {
        if (oldStatus == RsvpStatus.Accepted && ConfirmedCount > 0)
            ConfirmedCount--;
        else if (oldStatus == RsvpStatus.Declined && DeclinedCount > 0)
            DeclinedCount--;

        if (newStatus == RsvpStatus.Accepted)
            ConfirmedCount++;
        else if (newStatus == RsvpStatus.Declined)
            DeclinedCount++;

        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing handler - can be implemented if needed
    }
}
