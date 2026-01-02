using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SchoolConnect.Common.Infrastructure.EventStore;

public class StoredEvent
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [BsonRepresentation(BsonType.String)]
    public Guid AggregateId { get; set; }

    public string AggregateType { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string EventData { get; set; } = string.Empty;
    public int Version { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
