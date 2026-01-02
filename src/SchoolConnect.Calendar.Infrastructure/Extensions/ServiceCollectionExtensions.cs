using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SchoolConnect.Calendar.Domain.Interfaces;
using SchoolConnect.Calendar.Infrastructure.Repositories;
using SchoolConnect.Calendar.Infrastructure.Services;
using SchoolConnect.Calendar.Application.Services;

namespace SchoolConnect.Calendar.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCalendarInfrastructure(this IServiceCollection services, string connectionString, string databaseName)
    {
        // MongoDB configuration
        services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));
        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseName);
        });

        // Repositories
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITimetableRepository, TimetableRepository>();
        services.AddScoped<ITimetableSlotRepository, TimetableSlotRepository>();
        services.AddScoped<IReminderRepository, ReminderRepository>();

        // Services
        services.AddScoped<IConflictDetectionService, ConflictDetectionService>();
        services.AddScoped<IReminderSchedulerService, ReminderSchedulerService>();

        return services;
    }
}
