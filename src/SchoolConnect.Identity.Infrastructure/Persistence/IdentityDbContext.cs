using MongoDB.Driver;
using SchoolConnect.Identity.Domain.Entities;

namespace SchoolConnect.Identity.Infrastructure.Persistence;

public class IdentityDbContext
{
    private readonly IMongoDatabase _database;

    public IdentityDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("users");
    public IMongoCollection<Role> Roles => _database.GetCollection<Role>("roles");
    public IMongoCollection<Permission> Permissions => _database.GetCollection<Permission>("permissions");
    public IMongoCollection<UserRole> UserRoles => _database.GetCollection<UserRole>("userRoles");
    public IMongoCollection<RefreshToken> RefreshTokens => _database.GetCollection<RefreshToken>("refreshTokens");
    public IMongoCollection<Session> Sessions => _database.GetCollection<Session>("sessions");
    public IMongoCollection<ApiKey> ApiKeys => _database.GetCollection<ApiKey>("apiKeys");
}
