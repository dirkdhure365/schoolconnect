using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SchoolConnect.Enrolment.Domain.Interfaces;
using SchoolConnect.Enrolment.Infrastructure.Persistence;
using SchoolConnect.Enrolment.Infrastructure.Repositories;
using SchoolConnect.Enrolment.Application.ReadModels;
using SchoolConnect.Enrolment.Infrastructure.ReadModels;
using SchoolConnect.Enrolment.Application.EventHandlers;
using SchoolConnect.Common.Domain.Interfaces;
using SchoolConnect.Enrolment.Domain.Events;
using SchoolConnect.Common.Infrastructure.EventDispatcher;

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

        // Register Domain Event Dispatcher
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        // Register Repositories
        services.AddScoped<IAdmissionPeriodRepository, AdmissionPeriodRepository>();
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStreamRepository, StreamRepository>();
        services.AddScoped<ICohortRepository, CohortRepository>();
        services.AddScoped<IClassRepository, ClassRepository>();

        // Register Read Model Repositories
        services.AddScoped<IStudentReadModelRepository, StudentReadModelRepository>();
        services.AddScoped<IStudentEnrolmentSummaryReadModelRepository, StudentEnrolmentSummaryReadModelRepository>();

        // Register Event Handlers
        services.AddScoped<IDomainEventHandler<StudentCreatedEvent>, StudentCreatedEventHandler>();
        services.AddScoped<IDomainEventHandler<StudentUpdatedEvent>, StudentUpdatedEventHandler>();
        services.AddScoped<IDomainEventHandler<StudentEnrolledEvent>, StudentEnrolledEventHandler>();
        services.AddScoped<IDomainEventHandler<StudentWithdrawnEvent>, StudentWithdrawnEventHandler>();

        return services;
    }
}
