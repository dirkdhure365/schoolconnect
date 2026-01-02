using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SchoolConnect.Collaboration.Domain.Interfaces;
using SchoolConnect.Collaboration.Infrastructure.Persistence;
using SchoolConnect.Collaboration.Infrastructure.Repositories;

namespace SchoolConnect.Collaboration.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCollaborationInfrastructure(
        this IServiceCollection services,
        string connectionString,
        string databaseName)
    {
        // Register MongoDB
        services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));
        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseName);
        });
        
        // Register DbContext
        services.AddScoped<CollaborationDbContext>();
        
        // Register Repositories
        services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
        services.AddScoped<IBoardRepository, BoardRepository>();
        services.AddScoped<IListRepository, ListRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<ISharedResourceRepository, SharedResourceRepository>();
        
        return services;
    }
}
