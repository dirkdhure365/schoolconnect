using MongoDB.Driver;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Interfaces;
using SchoolConnect.Institution.Infrastructure.Persistence;

namespace SchoolConnect.Institution.Infrastructure.Repositories;

public class InstituteRepository : IInstituteRepository
{
    private readonly IMongoCollection<Institute> _collection;
    
    public InstituteRepository(IInstitutionDbContext context)
    {
        _collection = context.GetCollection<Institute>("institutes");
    }
    
    public async Task<Institute?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<Institute?> GetByCodeAsync(string code)
    {
        return await _collection.Find(x => x.Code == code).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<Institute>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
    
    public async Task<IEnumerable<Institute>> GetByStatusAsync(InstituteStatus status)
    {
        return await _collection.Find(x => x.Status == status).ToListAsync();
    }
    
    public async Task AddAsync(Institute institute)
    {
        await _collection.InsertOneAsync(institute);
    }
    
    public async Task UpdateAsync(Institute institute)
    {
        await _collection.ReplaceOneAsync(x => x.Id == institute.Id, institute);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}

public class CentreRepository : ICentreRepository
{
    private readonly IMongoCollection<Centre> _collection;
    
    public CentreRepository(IInstitutionDbContext context)
    {
        _collection = context.GetCollection<Centre>("centres");
    }
    
    public async Task<Centre?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<Centre?> GetByCodeAsync(string code)
    {
        return await _collection.Find(x => x.Code == code).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<Centre>> GetByInstituteIdAsync(Guid instituteId)
    {
        return await _collection.Find(x => x.InstituteId == instituteId).ToListAsync();
    }
    
    public async Task<IEnumerable<Centre>> GetByStatusAsync(CentreStatus status)
    {
        return await _collection.Find(x => x.Status == status).ToListAsync();
    }
    
    public async Task AddAsync(Centre centre)
    {
        await _collection.InsertOneAsync(centre);
    }
    
    public async Task UpdateAsync(Centre centre)
    {
        await _collection.ReplaceOneAsync(x => x.Id == centre.Id, centre);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}

public class FacilityRepository : IFacilityRepository
{
    private readonly IMongoCollection<Facility> _collection;
    
    public FacilityRepository(IInstitutionDbContext context)
    {
        _collection = context.GetCollection<Facility>("facilities");
    }
    
    public async Task<Facility?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<Facility>> GetByCentreIdAsync(Guid centreId)
    {
        return await _collection.Find(x => x.CentreId == centreId).ToListAsync();
    }
    
    public async Task<IEnumerable<Facility>> GetByTypeAsync(FacilityType type)
    {
        return await _collection.Find(x => x.Type == type).ToListAsync();
    }
    
    public async Task<IEnumerable<Facility>> GetAvailableAsync(Guid centreId)
    {
        return await _collection.Find(x => x.CentreId == centreId && x.Status == FacilityStatus.Available).ToListAsync();
    }
    
    public async Task AddAsync(Facility facility)
    {
        await _collection.InsertOneAsync(facility);
    }
    
    public async Task UpdateAsync(Facility facility)
    {
        await _collection.ReplaceOneAsync(x => x.Id == facility.Id, facility);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}

public class FacilityBookingRepository : IFacilityBookingRepository
{
    private readonly IMongoCollection<FacilityBooking> _collection;
    
    public FacilityBookingRepository(IInstitutionDbContext context)
    {
        _collection = context.GetCollection<FacilityBooking>("facility_bookings");
    }
    
    public async Task<FacilityBooking?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<FacilityBooking>> GetByFacilityIdAsync(Guid facilityId)
    {
        return await _collection.Find(x => x.FacilityId == facilityId).ToListAsync();
    }
    
    public async Task<IEnumerable<FacilityBooking>> GetByBookedByAsync(Guid bookedBy)
    {
        return await _collection.Find(x => x.BookedBy == bookedBy).ToListAsync();
    }
    
    public async Task<IEnumerable<FacilityBooking>> GetConflictingBookingsAsync(Guid facilityId, DateTime startTime, DateTime endTime)
    {
        return await _collection.Find(x => 
            x.FacilityId == facilityId &&
            x.Status != BookingStatus.Cancelled &&
            ((x.StartTime >= startTime && x.StartTime < endTime) ||
             (x.EndTime > startTime && x.EndTime <= endTime) ||
             (x.StartTime <= startTime && x.EndTime >= endTime))
        ).ToListAsync();
    }
    
    public async Task AddAsync(FacilityBooking booking)
    {
        await _collection.InsertOneAsync(booking);
    }
    
    public async Task UpdateAsync(FacilityBooking booking)
    {
        await _collection.ReplaceOneAsync(x => x.Id == booking.Id, booking);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}

public class ResourceRepository : IResourceRepository
{
    private readonly IMongoCollection<Resource> _collection;
    
    public ResourceRepository(IInstitutionDbContext context)
    {
        _collection = context.GetCollection<Resource>("resources");
    }
    
    public async Task<Resource?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<Resource>> GetByCentreIdAsync(Guid centreId)
    {
        return await _collection.Find(x => x.CentreId == centreId).ToListAsync();
    }
    
    public async Task<IEnumerable<Resource>> GetByTypeAsync(ResourceType type)
    {
        return await _collection.Find(x => x.Type == type).ToListAsync();
    }
    
    public async Task<IEnumerable<Resource>> GetAvailableAsync(Guid centreId)
    {
        return await _collection.Find(x => x.CentreId == centreId && x.Status == ResourceStatus.Available).ToListAsync();
    }
    
    public async Task AddAsync(Resource resource)
    {
        await _collection.InsertOneAsync(resource);
    }
    
    public async Task UpdateAsync(Resource resource)
    {
        await _collection.ReplaceOneAsync(x => x.Id == resource.Id, resource);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}

public class ResourceAllocationRepository : IResourceAllocationRepository
{
    private readonly IMongoCollection<ResourceAllocation> _collection;
    
    public ResourceAllocationRepository(IInstitutionDbContext context)
    {
        _collection = context.GetCollection<ResourceAllocation>("resource_allocations");
    }
    
    public async Task<ResourceAllocation?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<ResourceAllocation>> GetByResourceIdAsync(Guid resourceId)
    {
        return await _collection.Find(x => x.ResourceId == resourceId).ToListAsync();
    }
    
    public async Task<IEnumerable<ResourceAllocation>> GetByAllocatedToIdAsync(Guid allocatedToId)
    {
        return await _collection.Find(x => x.AllocatedToId == allocatedToId).ToListAsync();
    }
    
    public async Task<IEnumerable<ResourceAllocation>> GetActiveAllocationsAsync()
    {
        return await _collection.Find(x => x.Status == AllocationStatus.Active).ToListAsync();
    }
    
    public async Task<IEnumerable<ResourceAllocation>> GetOverdueAllocationsAsync()
    {
        return await _collection.Find(x => x.Status == AllocationStatus.Overdue).ToListAsync();
    }
    
    public async Task AddAsync(ResourceAllocation allocation)
    {
        await _collection.InsertOneAsync(allocation);
    }
    
    public async Task UpdateAsync(ResourceAllocation allocation)
    {
        await _collection.ReplaceOneAsync(x => x.Id == allocation.Id, allocation);
    }
}

public class StaffRepository : IStaffRepository
{
    private readonly IMongoCollection<StaffMember> _collection;
    
    public StaffRepository(IInstitutionDbContext context)
    {
        _collection = context.GetCollection<StaffMember>("staff_members");
    }
    
    public async Task<StaffMember?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<StaffMember?> GetByEmployeeCodeAsync(string employeeCode)
    {
        return await _collection.Find(x => x.EmployeeCode == employeeCode).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<StaffMember>> GetByInstituteIdAsync(Guid instituteId)
    {
        return await _collection.Find(x => x.InstituteId == instituteId).ToListAsync();
    }
    
    public async Task<IEnumerable<StaffMember>> GetByCentreIdAsync(Guid centreId)
    {
        // This would require joining with StaffCentreAssignment collection
        // For now, returning empty list
        await Task.CompletedTask;
        return new List<StaffMember>();
    }
    
    public async Task<IEnumerable<StaffMember>> GetByStatusAsync(StaffStatus status)
    {
        return await _collection.Find(x => x.Status == status).ToListAsync();
    }
    
    public async Task AddAsync(StaffMember staff)
    {
        await _collection.InsertOneAsync(staff);
    }
    
    public async Task UpdateAsync(StaffMember staff)
    {
        await _collection.ReplaceOneAsync(x => x.Id == staff.Id, staff);
    }
}

public class StaffCentreAssignmentRepository : IStaffCentreAssignmentRepository
{
    private readonly IMongoCollection<StaffCentreAssignment> _collection;
    
    public StaffCentreAssignmentRepository(IInstitutionDbContext context)
    {
        _collection = context.GetCollection<StaffCentreAssignment>("staff_centre_assignments");
    }
    
    public async Task<StaffCentreAssignment?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<StaffCentreAssignment>> GetByStaffIdAsync(Guid staffId)
    {
        return await _collection.Find(x => x.StaffMemberId == staffId).ToListAsync();
    }
    
    public async Task<IEnumerable<StaffCentreAssignment>> GetByCentreIdAsync(Guid centreId)
    {
        return await _collection.Find(x => x.CentreId == centreId).ToListAsync();
    }
    
    public async Task<StaffCentreAssignment?> GetActiveAssignmentAsync(Guid staffId, Guid centreId)
    {
        return await _collection.Find(x => 
            x.StaffMemberId == staffId && 
            x.CentreId == centreId && 
            x.EndDate == null
        ).FirstOrDefaultAsync();
    }
    
    public async Task AddAsync(StaffCentreAssignment assignment)
    {
        await _collection.InsertOneAsync(assignment);
    }
    
    public async Task UpdateAsync(StaffCentreAssignment assignment)
    {
        await _collection.ReplaceOneAsync(x => x.Id == assignment.Id, assignment);
    }
}

public class TeamRepository : ITeamRepository
{
    private readonly IMongoCollection<Team> _collection;
    
    public TeamRepository(IInstitutionDbContext context)
    {
        _collection = context.GetCollection<Team>("teams");
    }
    
    public async Task<Team?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<Team>> GetByInstituteIdAsync(Guid instituteId)
    {
        return await _collection.Find(x => x.InstituteId == instituteId).ToListAsync();
    }
    
    public async Task<IEnumerable<Team>> GetByCentreIdAsync(Guid centreId)
    {
        return await _collection.Find(x => x.CentreId == centreId).ToListAsync();
    }
    
    public async Task<IEnumerable<Team>> GetByTypeAsync(TeamType type)
    {
        return await _collection.Find(x => x.Type == type).ToListAsync();
    }
    
    public async Task AddAsync(Team team)
    {
        await _collection.InsertOneAsync(team);
    }
    
    public async Task UpdateAsync(Team team)
    {
        await _collection.ReplaceOneAsync(x => x.Id == team.Id, team);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}

public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly IMongoCollection<TeamMember> _collection;
    
    public TeamMemberRepository(IInstitutionDbContext context)
    {
        _collection = context.GetCollection<TeamMember>("team_members");
    }
    
    public async Task<TeamMember?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<TeamMember>> GetByTeamIdAsync(Guid teamId)
    {
        return await _collection.Find(x => x.TeamId == teamId).ToListAsync();
    }
    
    public async Task<IEnumerable<TeamMember>> GetByStaffIdAsync(Guid staffId)
    {
        return await _collection.Find(x => x.StaffMemberId == staffId).ToListAsync();
    }
    
    public async Task<TeamMember?> GetActiveTeamMemberAsync(Guid teamId, Guid staffId)
    {
        return await _collection.Find(x => 
            x.TeamId == teamId && 
            x.StaffMemberId == staffId && 
            x.LeftAt == null
        ).FirstOrDefaultAsync();
    }
    
    public async Task AddAsync(TeamMember member)
    {
        await _collection.InsertOneAsync(member);
    }
    
    public async Task UpdateAsync(TeamMember member)
    {
        await _collection.ReplaceOneAsync(x => x.Id == member.Id, member);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}
