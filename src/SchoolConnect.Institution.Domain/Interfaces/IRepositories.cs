using SchoolConnect.Institution.Domain.Entities;

namespace SchoolConnect.Institution.Domain.Interfaces;

public interface IInstituteRepository
{
    Task<Institute?> GetByIdAsync(Guid id);
    Task<Institute?> GetByCodeAsync(string code);
    Task<IEnumerable<Institute>> GetAllAsync();
    Task<IEnumerable<Institute>> GetByStatusAsync(Enums.InstituteStatus status);
    Task AddAsync(Institute institute);
    Task UpdateAsync(Institute institute);
    Task DeleteAsync(Guid id);
}

public interface ICentreRepository
{
    Task<Centre?> GetByIdAsync(Guid id);
    Task<Centre?> GetByCodeAsync(string code);
    Task<IEnumerable<Centre>> GetByInstituteIdAsync(Guid instituteId);
    Task<IEnumerable<Centre>> GetByStatusAsync(Enums.CentreStatus status);
    Task AddAsync(Centre centre);
    Task UpdateAsync(Centre centre);
    Task DeleteAsync(Guid id);
}

public interface IFacilityRepository
{
    Task<Facility?> GetByIdAsync(Guid id);
    Task<IEnumerable<Facility>> GetByCentreIdAsync(Guid centreId);
    Task<IEnumerable<Facility>> GetByTypeAsync(Enums.FacilityType type);
    Task<IEnumerable<Facility>> GetAvailableAsync(Guid centreId);
    Task AddAsync(Facility facility);
    Task UpdateAsync(Facility facility);
    Task DeleteAsync(Guid id);
}

public interface IFacilityBookingRepository
{
    Task<FacilityBooking?> GetByIdAsync(Guid id);
    Task<IEnumerable<FacilityBooking>> GetByFacilityIdAsync(Guid facilityId);
    Task<IEnumerable<FacilityBooking>> GetByBookedByAsync(Guid bookedBy);
    Task<IEnumerable<FacilityBooking>> GetConflictingBookingsAsync(Guid facilityId, DateTime startTime, DateTime endTime);
    Task AddAsync(FacilityBooking booking);
    Task UpdateAsync(FacilityBooking booking);
    Task DeleteAsync(Guid id);
}

public interface IResourceRepository
{
    Task<Resource?> GetByIdAsync(Guid id);
    Task<IEnumerable<Resource>> GetByCentreIdAsync(Guid centreId);
    Task<IEnumerable<Resource>> GetByTypeAsync(Enums.ResourceType type);
    Task<IEnumerable<Resource>> GetAvailableAsync(Guid centreId);
    Task AddAsync(Resource resource);
    Task UpdateAsync(Resource resource);
    Task DeleteAsync(Guid id);
}

public interface IResourceAllocationRepository
{
    Task<ResourceAllocation?> GetByIdAsync(Guid id);
    Task<IEnumerable<ResourceAllocation>> GetByResourceIdAsync(Guid resourceId);
    Task<IEnumerable<ResourceAllocation>> GetByAllocatedToIdAsync(Guid allocatedToId);
    Task<IEnumerable<ResourceAllocation>> GetActiveAllocationsAsync();
    Task<IEnumerable<ResourceAllocation>> GetOverdueAllocationsAsync();
    Task AddAsync(ResourceAllocation allocation);
    Task UpdateAsync(ResourceAllocation allocation);
}

public interface IStaffRepository
{
    Task<StaffMember?> GetByIdAsync(Guid id);
    Task<StaffMember?> GetByEmployeeCodeAsync(string employeeCode);
    Task<IEnumerable<StaffMember>> GetByInstituteIdAsync(Guid instituteId);
    Task<IEnumerable<StaffMember>> GetByCentreIdAsync(Guid centreId);
    Task<IEnumerable<StaffMember>> GetByStatusAsync(Enums.StaffStatus status);
    Task AddAsync(StaffMember staff);
    Task UpdateAsync(StaffMember staff);
}

public interface IStaffCentreAssignmentRepository
{
    Task<StaffCentreAssignment?> GetByIdAsync(Guid id);
    Task<IEnumerable<StaffCentreAssignment>> GetByStaffIdAsync(Guid staffId);
    Task<IEnumerable<StaffCentreAssignment>> GetByCentreIdAsync(Guid centreId);
    Task<StaffCentreAssignment?> GetActiveAssignmentAsync(Guid staffId, Guid centreId);
    Task AddAsync(StaffCentreAssignment assignment);
    Task UpdateAsync(StaffCentreAssignment assignment);
}

public interface ITeamRepository
{
    Task<Team?> GetByIdAsync(Guid id);
    Task<IEnumerable<Team>> GetByInstituteIdAsync(Guid instituteId);
    Task<IEnumerable<Team>> GetByCentreIdAsync(Guid centreId);
    Task<IEnumerable<Team>> GetByTypeAsync(Enums.TeamType type);
    Task AddAsync(Team team);
    Task UpdateAsync(Team team);
    Task DeleteAsync(Guid id);
}

public interface ITeamMemberRepository
{
    Task<TeamMember?> GetByIdAsync(Guid id);
    Task<IEnumerable<TeamMember>> GetByTeamIdAsync(Guid teamId);
    Task<IEnumerable<TeamMember>> GetByStaffIdAsync(Guid staffId);
    Task<TeamMember?> GetActiveTeamMemberAsync(Guid teamId, Guid staffId);
    Task AddAsync(TeamMember member);
    Task UpdateAsync(TeamMember member);
    Task DeleteAsync(Guid id);
}
