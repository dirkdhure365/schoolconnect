using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Domain.Entities;

public class EventReminder : Entity
{
    public Guid EventId { get; private set; }
    public Guid UserId { get; private set; }
    
    public int MinutesBefore { get; private set; }
    public DateTime ReminderTime { get; private set; }
    public ReminderChannel Channel { get; private set; }
    
    public ReminderStatus Status { get; private set; }
    public DateTime? SentAt { get; private set; }
    public string? FailureReason { get; private set; }

    private EventReminder() { }

    public static EventReminder Create(
        Guid eventId,
        Guid userId,
        int minutesBefore,
        DateTime eventStartTime,
        ReminderChannel channel)
    {
        return new EventReminder
        {
            Id = Guid.NewGuid(),
            EventId = eventId,
            UserId = userId,
            MinutesBefore = minutesBefore,
            ReminderTime = eventStartTime.AddMinutes(-minutesBefore),
            Channel = channel,
            Status = ReminderStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void MarkAsSent()
    {
        Status = ReminderStatus.Sent;
        SentAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string reason)
    {
        Status = ReminderStatus.Failed;
        FailureReason = reason;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        Status = ReminderStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }
}
