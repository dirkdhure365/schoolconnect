using SchoolConnect.Collaboration.Domain.Enums;

namespace SchoolConnect.Collaboration.Application.DTOs;

public class CardActivityDto
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? UserAvatarUrl { get; set; }
    public ActivityType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
