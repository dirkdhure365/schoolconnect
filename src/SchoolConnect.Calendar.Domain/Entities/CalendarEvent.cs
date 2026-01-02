using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Calendar.Domain.Enums;
using SchoolConnect.Calendar.Domain.Events;
using SchoolConnect.Calendar.Domain.ValueObjects;

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
    
    public Guid CreatedByUserId { get; private set; }
    public string CreatedByName { get; private set; } = string.Empty;

    private CalendarEvent() { }

    public static CalendarEvent Create(
        Guid instituteId,
        string title,
        DateTime startTime,
        DateTime endTime,
        EventType type,
        EventVisibility visibility,
        Guid createdBy,
        string createdByName,
        Guid? centreId = null,
        Guid? classId = null,
        string? description = null,
        EventLocation? location = null,
        bool isAllDay = false,
        string? timezone = null,
        RecurrenceRule? recurrence = null,
        bool rsvpRequired = false,
        int? maxAttendees = null,
        DateTime? rsvpDeadline = null,
        string? color = null,
        string? iconUrl = null)
    {
        if (endTime <= startTime)
            throw new ArgumentException("End time must be after start time");

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
            Recurrence = recurrence,
            Type = type,
            Color = color,
            IconUrl = iconUrl,
            Visibility = visibility,
            RsvpRequired = rsvpRequired,
            MaxAttendees = maxAttendees,
            RsvpDeadline = rsvpDeadline,
            Status = EventStatus.Scheduled,
            CreatedByUserId = createdBy,
            CreatedByName = createdByName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
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
        EventVisibility? visibility = null,
        Guid? updatedBy = null)
    {
        if (title != null) Title = title;
        if (description != null) Description = description;
        if (location != null) Location = location;
        if (startTime.HasValue) StartTime = startTime.Value;
        if (endTime.HasValue) EndTime = endTime.Value;
        if (isAllDay.HasValue) IsAllDay = isAllDay.Value;
        if (type.HasValue) Type = type.Value;
        if (visibility.HasValue) Visibility = visibility.Value;

        UpdatedAt = DateTime.UtcNow;

        if (updatedBy.HasValue)
        {
            Apply(new EventUpdatedEvent(Id, updatedBy.Value));
        }
    }

    public void Cancel(string reason, Guid cancelledBy)
    {
        Status = EventStatus.Cancelled;
        CancellationReason = reason;
        UpdatedAt = DateTime.UtcNow;

        Apply(new EventCancelledEvent(Id, reason, cancelledBy));
    }

    public void Delete(Guid deletedBy)
    {
        Apply(new EventDeletedEvent(Id, deletedBy));
    }

    public void UpdateAttendeeCount(int confirmed, int declined)
    {
        ConfirmedCount = confirmed;
        DeclinedCount = declined;
        AttendeeCount = confirmed + declined;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}
