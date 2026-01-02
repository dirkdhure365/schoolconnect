using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Calendar.Domain.Enums;
using SchoolConnect.Calendar.Domain.Events;

namespace SchoolConnect.Calendar.Domain.Entities;

public class EventAttendee : Entity
{
    public Guid EventId { get; private set; }
    public Guid UserId { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public string? UserEmail { get; private set; }
    public string? AvatarUrl { get; private set; }
    public string? Role { get; private set; }
    
    public RsvpStatus RsvpStatus { get; private set; }
    public DateTime? RsvpAt { get; private set; }
    public string? RsvpNotes { get; private set; }
    
    public bool IsOrganizer { get; private set; }
    public bool NotificationSent { get; private set; }
    
    public DateTime AddedAt { get; private set; }
    public Guid AddedByUserId { get; private set; }

    private EventAttendee() { }

    public static EventAttendee Create(
        Guid eventId,
        Guid userId,
        string userName,
        Guid addedBy,
        string? userEmail = null,
        string? avatarUrl = null,
        string? role = null,
        bool isOrganizer = false)
    {
        var attendee = new EventAttendee
        {
            Id = Guid.NewGuid(),
            EventId = eventId,
            UserId = userId,
            UserName = userName,
            UserEmail = userEmail,
            AvatarUrl = avatarUrl,
            Role = role,
            IsOrganizer = isOrganizer,
            RsvpStatus = RsvpStatus.Pending,
            AddedAt = DateTime.UtcNow,
            AddedByUserId = addedBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return attendee;
    }

    public void UpdateRsvp(RsvpStatus status, string? notes = null)
    {
        RsvpStatus = status;
        RsvpAt = DateTime.UtcNow;
        RsvpNotes = notes;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkNotificationSent()
    {
        NotificationSent = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
