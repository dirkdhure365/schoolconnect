using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace SchoolConnect.Common.Infrastructure.Persistence;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string? collectionName = null)
    {
        var name = collectionName ?? typeof(T).Name;
        return _database.GetCollection<T>(name);
    }

    public IMongoDatabase Database => _database;
}
