namespace SchoolConnect.Communication.Application.DTOs;

public class ParticipantDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string? Role { get; set; }
    public DateTime JoinedAt { get; set; }
    public bool IsActive { get; set; }
    public int UnreadCount { get; set; }
    public bool IsMuted { get; set; }
}
