using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SchoolConnect.Enrolment.Domain.Interfaces;
using SchoolConnect.Enrolment.Infrastructure.Persistence;
using SchoolConnect.Enrolment.Infrastructure.Repositories;

namespace SchoolConnect.Enrolment.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEnrolmentInfrastructure(
        this IServiceCollection services,
        string connectionString,
        string databaseName = "SchoolConnectEnrolment")
    {
        // Register MongoDB client (if not already registered)
        if (!services.Any(x => x.ServiceType == typeof(IMongoClient)))
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                return new MongoClient(connectionString);
            });
        }

        // Register DbContext
        services.AddScoped(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            return new EnrolmentDbContext(mongoClient, databaseName);
        });

        // Register Repositories
        services.AddScoped<IAdmissionPeriodRepository, AdmissionPeriodRepository>();
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStreamRepository, StreamRepository>();
        services.AddScoped<ICohortRepository, CohortRepository>();
        services.AddScoped<IClassRepository, ClassRepository>();

        return services;
    }
}
