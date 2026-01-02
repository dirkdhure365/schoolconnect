using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SchoolConnect.Institution.Domain.Interfaces;
using SchoolConnect.Institution.Infrastructure.Persistence;
using SchoolConnect.Institution.Infrastructure.Repositories;

namespace SchoolConnect.Institution.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInstitutionInfrastructure(
        this IServiceCollection services,
        string connectionString,
        string databaseName = "SchoolConnectInstitution")
    {
        // Register MongoDB client
        services.AddSingleton<IMongoClient>(sp =>
        {
            return new MongoClient(connectionString);
        });

        // Register DbContext
        services.AddScoped(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            return new InstitutionDbContext(mongoClient, databaseName);
        });

        // Register Repositories
        services.AddScoped<IInstituteRepository, InstituteRepository>();
        services.AddScoped<ICentreRepository, CentreRepository>();
        services.AddScoped<IFacilityRepository, FacilityRepository>();
        services.AddScoped<IResourceRepository, ResourceRepository>();
        services.AddScoped<IStaffRepository, StaffRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();

        return services;
    }
}
