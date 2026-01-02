using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Entities;

public class ApiKey : Entity
{
    public Guid UserId { get; private set; }
    public string KeyHash { get; private set; }
    public string Name { get; private set; }
    public List<string> Scopes { get; private set; } = [];
    public DateTime? ExpiresAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public DateTime? LastUsedAt { get; private set; }

    private ApiKey() { }

    public ApiKey(Guid userId, string keyHash, string name, List<string>? scopes = null, DateTime? expiresAt = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        KeyHash = keyHash ?? throw new ArgumentNullException(nameof(keyHash));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Scopes = scopes ?? [];
        ExpiresAt = expiresAt;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsActive => !IsExpired && !IsRevoked;
    public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow >= ExpiresAt.Value;
    public bool IsRevoked => RevokedAt.HasValue;

    public void Revoke()
    {
        RevokedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RecordUsage()
    {
        LastUsedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
