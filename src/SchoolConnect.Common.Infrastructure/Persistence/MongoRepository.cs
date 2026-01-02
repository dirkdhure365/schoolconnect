using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SchoolConnect.Common.Application.Models;
using SchoolConnect.Common.Domain.Interfaces;
using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Common.Infrastructure.EventStore;
using SchoolConnect.Common.Infrastructure.Messaging.AzureServiceBus;

namespace SchoolConnect.Common.Infrastructure.Persistence;

public class MongoRepository<T> : IRepository<T> where T : AggregateRoot
{
    protected readonly IMongoCollection<T> Collection;
    protected readonly IEventStore? EventStore;
    protected readonly IMessagePublisher? MessagePublisher;
    protected readonly ILogger<MongoRepository<T>> _logger;

    public MongoRepository(
        MongoDbContext context,
        ILogger<MongoRepository<T>> logger,
        IEventStore? eventStore = null,
        IMessagePublisher? messagePublisher = null)
    {
        Collection = context.GetCollection<T>();
        _logger = logger;
        EventStore = eventStore;
        MessagePublisher = messagePublisher;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var filter = Builders<T>.Filter.Eq(e => e.Id, id);
        return await Collection.Find(filter).FirstOrDefaultAsync(ct);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
    {
        return await Collection.Find(_ => true).ToListAsync(ct);
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
    {
        return await Collection.Find(predicate).ToListAsync(ct);
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken ct = default)
    {
        await Collection.InsertOneAsync(entity, cancellationToken: ct);

        // Save domain events to event store if available
        if (EventStore != null && entity.DomainEvents.Any())
        {
            await EventStore.SaveEventsAsync(entity.Id, entity.DomainEvents, entity.Version - entity.DomainEvents.Count, ct);
        }

        entity.ClearDomainEvents();

        _logger.LogInformation("Added entity {EntityType} with ID {EntityId}", typeof(T).Name, entity.Id);

        return entity;
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
        await Collection.ReplaceOneAsync(filter, entity, cancellationToken: ct);

        // Save domain events to event store if available
        if (EventStore != null && entity.DomainEvents.Any())
        {
            await EventStore.SaveEventsAsync(entity.Id, entity.DomainEvents, entity.Version - entity.DomainEvents.Count, ct);
        }

        entity.ClearDomainEvents();

        _logger.LogInformation("Updated entity {EntityType} with ID {EntityId}", typeof(T).Name, entity.Id);
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var filter = Builders<T>.Filter.Eq(e => e.Id, id);
        await Collection.DeleteOneAsync(filter, ct);

        _logger.LogInformation("Deleted entity {EntityType} with ID {EntityId}", typeof(T).Name, id);
    }

    public virtual async Task<PagedResult<T>> GetPagedAsync(PagedRequest request, CancellationToken ct = default)
    {
        var filter = Builders<T>.Filter.Empty;

        // Apply search filter if provided (this is a simple implementation)
        // In a real scenario, you'd want to make this configurable per entity type

        var totalCount = await Collection.CountDocumentsAsync(filter, cancellationToken: ct);

        var sortDefinition = string.IsNullOrEmpty(request.SortBy)
            ? Builders<T>.Sort.Descending(e => e.CreatedAt)
            : request.SortDescending
                ? Builders<T>.Sort.Descending(request.SortBy)
                : Builders<T>.Sort.Ascending(request.SortBy);

        var items = await Collection.Find(filter)
            .Sort(sortDefinition)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Limit(request.PageSize)
            .ToListAsync(ct);

        return new PagedResult<T>(items, (int)totalCount, request.PageNumber, request.PageSize);
    }
}
