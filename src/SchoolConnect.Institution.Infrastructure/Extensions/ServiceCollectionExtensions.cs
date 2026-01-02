using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SchoolConnect.Institution.Domain.Interfaces;
using SchoolConnect.Institution.Infrastructure.Persistence;
using SchoolConnect.Institution.Infrastructure.Repositories;
using SchoolConnect.Institution.Infrastructure.Seed;

namespace SchoolConnect.Institution.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInstitutionInfrastructure(
        this IServiceCollection services,
        string connectionString,
        string databaseName = "SchoolConnectInstitution")
        string databaseName)
    {
        // Register MongoDB client
        services.AddSingleton<IMongoClient>(sp =>
        {
            return new MongoClient(connectionString);
        });

        // Register DbContext
        services.AddSingleton<IInstitutionDbContext>(
            sp => new InstitutionDbContext(connectionString, databaseName));

        // Register Repositories
        services.AddSingleton<IInstituteRepository, InstituteRepository>();
        services.AddSingleton<ICentreRepository, CentreRepository>();
        services.AddSingleton<IFacilityRepository, FacilityRepository>();
        services.AddSingleton<IFacilityBookingRepository, FacilityBookingRepository>();
        services.AddSingleton<IResourceRepository, ResourceRepository>();
        services.AddSingleton<IResourceAllocationRepository, ResourceAllocationRepository>();
        services.AddSingleton<IStaffRepository, StaffRepository>();
        services.AddSingleton<IStaffCentreAssignmentRepository, StaffCentreAssignmentRepository>();
        services.AddSingleton<ITeamRepository, TeamRepository>();
        services.AddSingleton<ITeamMemberRepository, TeamMemberRepository>();
        
        // Register Seed Service
        services.AddSingleton<InstitutionSeedService>();

        return services;
    }
}
