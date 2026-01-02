namespace SchoolConnect.Identity.Application.DTOs;

public class SessionDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string DeviceInfo { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string? UserAgent { get; set; }
    public string? Location { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime LastActivityAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public bool IsActive { get; set; }
}
