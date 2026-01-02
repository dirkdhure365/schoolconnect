using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Application.DTOs;

public class CalendarEventDto
{
    public Guid Id { get; set; }
    public Guid InstituteId { get; set; }
    public Guid? CentreId { get; set; }
    public Guid? ClassId { get; set; }
    
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public EventLocationDto? Location { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsAllDay { get; set; }
    public string? Timezone { get; set; }
    
    public RecurrenceRuleDto? Recurrence { get; set; }
    public Guid? RecurrenceGroupId { get; set; }
    
    public EventType Type { get; set; }
    public string? Color { get; set; }
    public string? IconUrl { get; set; }
    
    public EventVisibility Visibility { get; set; }
    public bool RsvpRequired { get; set; }
    public int? MaxAttendees { get; set; }
    public DateTime? RsvpDeadline { get; set; }
    
    public int AttendeeCount { get; set; }
    public int ConfirmedCount { get; set; }
    public int DeclinedCount { get; set; }
    
    public List<string> AttachmentUrls { get; set; } = [];
    
    public EventStatus Status { get; set; }
    public string? CancellationReason { get; set; }
    
    public Guid CreatedBy { get; set; }
    public string CreatedByName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class EventLocationDto
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public Guid? FacilityId { get; set; }
    public string? FacilityName { get; set; }
    public string? VirtualMeetingUrl { get; set; }
    public string? VirtualMeetingProvider { get; set; }
}

public class RecurrenceRuleDto
{
    public string Frequency { get; set; } = string.Empty;
    public int Interval { get; set; }
    public List<DayOfWeek>? DaysOfWeek { get; set; }
    public List<int>? DaysOfMonth { get; set; }
    public List<int>? MonthsOfYear { get; set; }
    public DateTime? EndDate { get; set; }
    public int? Occurrences { get; set; }
}
