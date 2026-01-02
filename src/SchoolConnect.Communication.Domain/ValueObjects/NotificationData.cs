using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Communication.Domain.ValueObjects;

public class NotificationData : ValueObject
{
    public string EntityType { get; private set; } = string.Empty;
    public Guid EntityId { get; private set; }
    public Dictionary<string, string> Metadata { get; private set; } = new();

    private NotificationData() { }

    public static NotificationData Create(
        string entityType,
        Guid entityId,
        Dictionary<string, string>? metadata = null)
    {
        return new NotificationData
        {
            EntityType = entityType,
            EntityId = entityId,
            Metadata = metadata ?? new Dictionary<string, string>()
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return EntityType;
        yield return EntityId;
        foreach (var kvp in Metadata.OrderBy(x => x.Key))
        {
            yield return kvp.Key;
            yield return kvp.Value;
        }
    }
}
