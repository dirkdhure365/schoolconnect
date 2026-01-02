using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Application.DTOs;

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
    public string? FailureReason { get; set; }
}
