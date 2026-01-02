using MongoDB.Driver;

namespace SchoolConnect.Institution.Infrastructure.Persistence;

public interface IInstitutionDbContext
{
    IMongoCollection<T> GetCollection<T>(string name);
}

public class InstitutionDbContext : IInstitutionDbContext
{
    private readonly IMongoDatabase _database;
    
    public InstitutionDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }
    
    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}
