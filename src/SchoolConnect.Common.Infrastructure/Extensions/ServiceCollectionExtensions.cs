using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolConnect.Common.Infrastructure.EventStore;
using SchoolConnect.Common.Infrastructure.Messaging.AzureServiceBus;
using SchoolConnect.Common.Infrastructure.Persistence;

namespace SchoolConnect.Common.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = "MongoDB")
    {
        services.Configure<MongoDbSettings>(configuration.GetSection(sectionName));
        services.AddSingleton<MongoDbContext>();
        services.AddSingleton<MongoIndexManager>();

        return services;
    }

    public static IServiceCollection AddEventStore(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = "EventStore")
    {
        services.Configure<EventStoreSettings>(configuration.GetSection(sectionName));
        services.AddSingleton<IEventStore, MongoEventStore>();

        return services;
    }

    public static IServiceCollection AddServiceBusMessaging(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = "ServiceBus")
    {
        services.Configure<ServiceBusSettings>(configuration.GetSection(sectionName));
        services.AddSingleton<IMessagePublisher, ServiceBusPublisher>();
        services.AddSingleton<IMessageConsumer, ServiceBusConsumer>();

        return services;
    }
}
