using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SchoolConnect.Communication.Domain.Interfaces;
using SchoolConnect.Communication.Infrastructure.Persistence;
using SchoolConnect.Communication.Infrastructure.Repositories;

namespace SchoolConnect.Communication.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommunicationInfrastructure(
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
        services.AddScoped<CommunicationDbContext>();
        
        // Register Repositories
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IConversationRepository, ConversationRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
        services.AddScoped<IFeedRepository, FeedRepository>();
        
        return services;
    }
}
