using MongoDB.Driver;
using SchoolConnect.EducationSystem.Application.Interfaces;
using SchoolConnect.EducationSystem.Domain.Aggregates;
using SchoolConnect.EducationSystem.Infrastructure.Persistence;

namespace SchoolConnect.EducationSystem.Infrastructure.Repositories;

public class MongoRepository<T> : Application.Interfaces.IRepository<T> where T : AggregateRoot
{
    private readonly IMongoCollection<T> _collection;
    private readonly Application.Interfaces.IEventStore _eventStore;

    public MongoRepository(IMongoDbContext context, Application.Interfaces.IEventStore eventStore, string collectionName)
    {
        _collection = context.GetCollection<T>(collectionName);
        _eventStore = eventStore;
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq(e => e.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        
        // Store events
        foreach (var domainEvent in entity.DomainEvents)
        {
            await _eventStore.SaveEventAsync(domainEvent);
        }
        
        entity.ClearDomainEvents();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter, entity);
        
        // Store events
        foreach (var domainEvent in entity.DomainEvents)
        {
            await _eventStore.SaveEventAsync(domainEvent);
        }
        
        entity.ClearDomainEvents();
        return entity;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq(e => e.Id, id);
        var result = await _collection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
