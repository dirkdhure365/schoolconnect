using SchoolConnect.Communication.Domain.Enums;

namespace SchoolConnect.Communication.Application.DTOs;

public class ConversationDto
{
    public Guid Id { get; set; }
    public ConversationType Type { get; set; }
    public string? Title { get; set; }
    public string? AvatarUrl { get; set; }
    public Guid? InstituteId { get; set; }
    public Guid? ClassId { get; set; }
    public List<ParticipantDto> Participants { get; set; } = [];
    public Guid? LastMessageId { get; set; }
    public string? LastMessagePreview { get; set; }
    public Guid? LastMessageSenderId { get; set; }
    public DateTime? LastMessageAt { get; set; }
    public int MessageCount { get; set; }
    public bool IsArchived { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class ConversationSummaryDto
{
    public Guid Id { get; set; }
    public ConversationType Type { get; set; }
    public string? Title { get; set; }
    public string? AvatarUrl { get; set; }
    public string? LastMessagePreview { get; set; }
    public DateTime? LastMessageAt { get; set; }
    public int UnreadCount { get; set; }
}
