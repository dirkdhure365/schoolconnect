using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Application.DTOs;

public class EventAttendeeDto
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? UserEmail { get; set; }
    public string? Role { get; set; }
    
    public RsvpStatus RsvpStatus { get; set; }
    public DateTime? RsvpAt { get; set; }
    public string? RsvpNotes { get; set; }
    
    public bool IsOrganizer { get; set; }
    public DateTime AddedAt { get; set; }
}

public class EventReminderDto
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }
    
    public int MinutesBefore { get; set; }
    public DateTime ReminderTime { get; set; }
    public ReminderChannel Channel { get; set; }
    
    public ReminderStatus Status { get; set; }
    public DateTime? SentAt { get; set; }
}
