using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Common.Infrastructure.EventStore;

public class MongoEventStore : IEventStore
{
    private readonly IMongoCollection<StoredEvent> _events;
    private readonly ILogger<MongoEventStore> _logger;

    public MongoEventStore(
        IOptions<EventStoreSettings> settings,
        ILogger<MongoEventStore> logger)
    {
        _logger = logger;
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _events = database.GetCollection<StoredEvent>(settings.Value.CollectionName);

        // Create indexes
        CreateIndexes();
    }

    private void CreateIndexes()
    {
        var indexKeysDefinition = Builders<StoredEvent>.IndexKeys
            .Ascending(e => e.AggregateId)
            .Ascending(e => e.Version);

        var indexModel = new CreateIndexModel<StoredEvent>(indexKeysDefinition);
        _events.Indexes.CreateOne(indexModel);
    }

    public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<DomainEvent> events, int expectedVersion, CancellationToken ct = default)
    {
        var eventList = events.ToList();
        if (!eventList.Any()) return;

        // Check for concurrency conflicts
        var lastEvent = await _events
            .Find(e => e.AggregateId == aggregateId)
            .SortByDescending(e => e.Version)
            .FirstOrDefaultAsync(ct);

        if (lastEvent != null && lastEvent.Version != expectedVersion)
        {
            throw new InvalidOperationException($"Concurrency conflict for aggregate {aggregateId}. Expected version {expectedVersion}, but found {lastEvent.Version}");
        }

        var storedEvents = eventList.Select((e, index) => new StoredEvent
        {
            AggregateId = aggregateId,
            AggregateType = e.AggregateType,
            EventType = e.GetType().Name,
            EventData = JsonSerializer.Serialize(e, e.GetType()),
            Version = expectedVersion + index + 1,
            Timestamp = e.OccurredOn
        }).ToList();

        await _events.InsertManyAsync(storedEvents, cancellationToken: ct);

        _logger.LogInformation("Saved {Count} events for aggregate {AggregateId}", eventList.Count, aggregateId);
    }

    public async Task<IEnumerable<DomainEvent>> GetEventsAsync(Guid aggregateId, CancellationToken ct = default)
    {
        var storedEvents = await _events
            .Find(e => e.AggregateId == aggregateId)
            .SortBy(e => e.Version)
            .ToListAsync(ct);

        return DeserializeEvents(storedEvents);
    }

    public async Task<IEnumerable<DomainEvent>> GetEventsAfterAsync(Guid aggregateId, int version, CancellationToken ct = default)
    {
        var storedEvents = await _events
            .Find(e => e.AggregateId == aggregateId && e.Version > version)
            .SortBy(e => e.Version)
            .ToListAsync(ct);

        return DeserializeEvents(storedEvents);
    }

    private static IEnumerable<DomainEvent> DeserializeEvents(IEnumerable<StoredEvent> storedEvents)
    {
        var domainEvents = new List<DomainEvent>();

        foreach (var storedEvent in storedEvents)
        {
            // In a real implementation, you would need a type registry to map event type names to actual types
            // For now, this is a simplified version
            // You might want to use reflection or a type registry pattern here
            
            // This is a placeholder - actual implementation would require proper type resolution
            // domainEvents.Add(JsonSerializer.Deserialize<DomainEvent>(storedEvent.EventData));
        }

        return domainEvents;
    }
}
