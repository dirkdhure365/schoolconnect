using SchoolConnect.Institution.Domain.Entities;

namespace SchoolConnect.Institution.Domain.Interfaces;

public interface IStaffRepository
{
    Task<StaffMember?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<StaffMember?> GetByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default);
    Task<List<StaffMember>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default);
    Task<List<StaffMember>> GetByCentreIdAsync(Guid centreId, CancellationToken cancellationToken = default);
    Task<StaffMember> AddAsync(StaffMember staffMember, CancellationToken cancellationToken = default);
    Task UpdateAsync(StaffMember staffMember, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<StaffCentreAssignment>> GetCentreAssignmentsAsync(Guid staffMemberId, CancellationToken cancellationToken = default);
    Task<StaffCentreAssignment> AddCentreAssignmentAsync(StaffCentreAssignment assignment, CancellationToken cancellationToken = default);
    Task RemoveCentreAssignmentAsync(Guid staffMemberId, Guid centreId, CancellationToken cancellationToken = default);
}
