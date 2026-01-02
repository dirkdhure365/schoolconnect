using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SchoolConnect.Subscription.Domain.Interfaces;
using SchoolConnect.Subscription.Infrastructure.Persistence;
using SchoolConnect.Subscription.Infrastructure.Repositories;

namespace SchoolConnect.Subscription.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSubscriptionInfrastructure(
        this IServiceCollection services,
        string connectionString,
        string databaseName)
    {
        // Register MongoDB client
        services.AddSingleton<IMongoClient>(sp =>
        {
            return new MongoClient(connectionString);
        });

        // Register DbContext
        services.AddSingleton(sp => new SubscriptionDbContext(connectionString, databaseName));

        // Register Repositories
        services.AddSingleton<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
        services.AddSingleton<ISubscriptionRepository, SubscriptionRepository>();
        services.AddSingleton<ISubscriptionUsageRepository, SubscriptionUsageRepository>();
        services.AddSingleton<ISubscriptionTrialRepository, SubscriptionTrialRepository>();

        return services;
    }
}
