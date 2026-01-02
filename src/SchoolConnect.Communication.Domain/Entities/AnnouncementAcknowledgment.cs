using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Communication.Domain.Events;

namespace SchoolConnect.Communication.Domain.Entities;

public class AnnouncementAcknowledgment : Entity
{
    public Guid AnnouncementId { get; private set; }
    public Guid UserId { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public string? UserRole { get; private set; }
    public DateTime AcknowledgedAt { get; private set; }
    public string? IpAddress { get; private set; }
    public string? DeviceInfo { get; private set; }

    private AnnouncementAcknowledgment() { }

    public static AnnouncementAcknowledgment Create(
        Guid announcementId,
        Guid userId,
        string userName,
        string? userRole = null,
        string? ipAddress = null,
        string? deviceInfo = null)
    {
        return new AnnouncementAcknowledgment
        {
            Id = Guid.NewGuid(),
            AnnouncementId = announcementId,
            UserId = userId,
            UserName = userName,
            UserRole = userRole,
            AcknowledgedAt = DateTime.UtcNow,
            IpAddress = ipAddress,
            DeviceInfo = deviceInfo,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
