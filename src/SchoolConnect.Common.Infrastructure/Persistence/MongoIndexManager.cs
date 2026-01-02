using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace SchoolConnect.Common.Infrastructure.Persistence;

public class MongoIndexManager
{
    private readonly MongoDbContext _context;
    private readonly ILogger<MongoIndexManager> _logger;

    public MongoIndexManager(MongoDbContext context, ILogger<MongoIndexManager> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task CreateIndexAsync<T>(
        Expression<Func<T, object>> field,
        bool ascending = true,
        bool unique = false,
        string? collectionName = null)
    {
        var collection = _context.GetCollection<T>(collectionName);
        
        var indexKeysDefinition = ascending
            ? Builders<T>.IndexKeys.Ascending(field)
            : Builders<T>.IndexKeys.Descending(field);

        var indexOptions = new CreateIndexOptions { Unique = unique };
        var indexModel = new CreateIndexModel<T>(indexKeysDefinition, indexOptions);

        await collection.Indexes.CreateOneAsync(indexModel);

        _logger.LogInformation("Created index on {FieldName} for collection {CollectionName}",
            field.ToString(), collectionName ?? typeof(T).Name);
    }

    public async Task CreateCompoundIndexAsync<T>(
        IEnumerable<(Expression<Func<T, object>> Field, bool Ascending)> fields,
        bool unique = false,
        string? collectionName = null)
    {
        var collection = _context.GetCollection<T>(collectionName);
        
        var indexKeysDefinitionBuilder = Builders<T>.IndexKeys;
        IndexKeysDefinition<T>? indexKeysDefinition = null;

        foreach (var (field, ascending) in fields)
        {
            var currentKey = ascending
                ? indexKeysDefinitionBuilder.Ascending(field)
                : indexKeysDefinitionBuilder.Descending(field);

            indexKeysDefinition = indexKeysDefinition == null
                ? currentKey
                : Builders<T>.IndexKeys.Combine(indexKeysDefinition, currentKey);
        }

        if (indexKeysDefinition != null)
        {
            var indexOptions = new CreateIndexOptions { Unique = unique };
            var indexModel = new CreateIndexModel<T>(indexKeysDefinition, indexOptions);

            await collection.Indexes.CreateOneAsync(indexModel);

            _logger.LogInformation("Created compound index for collection {CollectionName}",
                collectionName ?? typeof(T).Name);
        }
    }
}
