using MongoDB.Driver;
using SchoolConnect.Institution.Domain.Entities;

namespace SchoolConnect.Institution.Infrastructure.Persistence;

public class InstitutionDbContext
{
    private readonly IMongoDatabase _database;

    public InstitutionDbContext(IMongoClient mongoClient, string databaseName = "SchoolConnectInstitution")
    {
        _database = mongoClient.GetDatabase(databaseName);
    }

    public IMongoCollection<Institute> Institutes => _database.GetCollection<Institute>("institutes");
    public IMongoCollection<Centre> Centres => _database.GetCollection<Centre>("centres");
    public IMongoCollection<Facility> Facilities => _database.GetCollection<Facility>("facilities");
    public IMongoCollection<FacilityBooking> FacilityBookings => _database.GetCollection<FacilityBooking>("facility_bookings");
    public IMongoCollection<Resource> Resources => _database.GetCollection<Resource>("resources");
    public IMongoCollection<ResourceAllocation> ResourceAllocations => _database.GetCollection<ResourceAllocation>("resource_allocations");
    public IMongoCollection<StaffMember> StaffMembers => _database.GetCollection<StaffMember>("staff_members");
    public IMongoCollection<StaffCentreAssignment> StaffCentreAssignments => _database.GetCollection<StaffCentreAssignment>("staff_centre_assignments");
    public IMongoCollection<Team> Teams => _database.GetCollection<Team>("teams");
    public IMongoCollection<TeamMember> TeamMembers => _database.GetCollection<TeamMember>("team_members");
}
