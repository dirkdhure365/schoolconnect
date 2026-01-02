using MongoDB.Driver;
using SchoolConnect.EducationSystem.Domain.Events;
using SchoolConnect.EducationSystem.Infrastructure.Persistence;

namespace SchoolConnect.EducationSystem.Infrastructure.EventStore;

public class MongoEventStore : Application.Interfaces.IEventStore
{
    private readonly IMongoCollection<DomainEvent> _eventCollection;

    public MongoEventStore(IMongoDbContext context)
    {
        _eventCollection = context.GetCollection<DomainEvent>("Events");
    }

    public async Task SaveEventAsync(DomainEvent domainEvent)
    {
        await _eventCollection.InsertOneAsync(domainEvent);
    }

    public async Task<List<DomainEvent>> GetEventsAsync()
    {
        return await _eventCollection.Find(_ => true).ToListAsync();
    }

    public async Task<List<DomainEvent>> GetEventsByAggregateAsync(string aggregateId)
    {
        var filter = Builders<DomainEvent>.Filter.Eq(e => e.AggregateId, aggregateId);
        return await _eventCollection.Find(filter).ToListAsync();
    }
}
