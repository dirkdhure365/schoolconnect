using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SchoolConnect.LessonDelivery.Domain.Interfaces;
using SchoolConnect.LessonDelivery.Infrastructure.Persistence;
using SchoolConnect.LessonDelivery.Infrastructure.Repositories;

namespace SchoolConnect.LessonDelivery.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLessonDeliveryInfrastructure(
        this IServiceCollection services,
        string connectionString,
        string databaseName = "SchoolConnectLessonDelivery")
    {
        // Register MongoDB client
        services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));
        
        // Register DbContext
        services.AddScoped(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            return new LessonDeliveryDbContext(mongoClient, databaseName);
        });
        
        // Register repositories
        services.AddScoped<ILessonPlanRepository, LessonPlanRepository>();
        services.AddScoped<ILessonTemplateRepository, LessonTemplateRepository>();
        services.AddScoped<IScheduledLessonRepository, ScheduledLessonRepository>();
        services.AddScoped<ILessonSessionRepository, LessonSessionRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<IHomeworkRepository, HomeworkRepository>();
        services.AddScoped<ICurriculumCoverageRepository, CurriculumCoverageRepository>();
        
        return services;
    }
}
