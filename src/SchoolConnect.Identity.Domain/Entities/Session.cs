using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Identity.Domain.Events;

namespace SchoolConnect.Identity.Domain.Entities;

public class Session : Entity
{
    public Guid UserId { get; private set; }
    public string DeviceInfo { get; private set; }
    public string IpAddress { get; private set; }
    public string? UserAgent { get; private set; }
    public string? Location { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime LastActivityAt { get; private set; }
    public DateTime? EndedAt { get; private set; }

    private Session() { }

    public Session(Guid userId, string deviceInfo, string ipAddress, string? userAgent = null, string? location = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        DeviceInfo = deviceInfo ?? throw new ArgumentNullException(nameof(deviceInfo));
        IpAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));
        UserAgent = userAgent;
        Location = location;
        StartedAt = DateTime.UtcNow;
        LastActivityAt = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsActive => EndedAt == null;

    public void UpdateActivity()
    {
        LastActivityAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void End()
    {
        if (EndedAt == null)
        {
            EndedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
