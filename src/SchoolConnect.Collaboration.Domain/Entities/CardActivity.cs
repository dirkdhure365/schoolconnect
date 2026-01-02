using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Collaboration.Domain.Enums;

namespace SchoolConnect.Collaboration.Domain.Entities;

public class CardActivity : Entity
{
    public Guid CardId { get; private set; }
    public Guid BoardId { get; private set; }
    
    public Guid UserId { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public string? UserAvatarUrl { get; private set; }
    
    public ActivityType Type { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public Dictionary<string, object> Data { get; private set; } = new();

    private CardActivity() { }

    public static CardActivity Create(
        Guid cardId,
        Guid boardId,
        Guid userId,
        string userName,
        ActivityType type,
        string description,
        string? userAvatarUrl = null,
        Dictionary<string, object>? data = null)
    {
        return new CardActivity
        {
            Id = Guid.NewGuid(),
            CardId = cardId,
            BoardId = boardId,
            UserId = userId,
            UserName = userName,
            UserAvatarUrl = userAvatarUrl,
            Type = type,
            Description = description,
            Data = data ?? new Dictionary<string, object>(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
