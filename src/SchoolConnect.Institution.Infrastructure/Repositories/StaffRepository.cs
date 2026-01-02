using MongoDB.Driver;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Interfaces;
using SchoolConnect.Institution.Infrastructure.Persistence;

namespace SchoolConnect.Institution.Infrastructure.Repositories;

public class StaffRepository : IStaffRepository
{
    private readonly InstitutionDbContext _context;

    public StaffRepository(InstitutionDbContext context)
    {
        _context = context;
    }

    public async Task<StaffMember?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.StaffMembers
            .Find(s => s.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<StaffMember?> GetByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default)
    {
        return await _context.StaffMembers
            .Find(s => s.EmployeeCode == employeeCode)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<StaffMember>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _context.StaffMembers
            .Find(s => s.InstituteId == instituteId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<StaffMember>> GetByCentreIdAsync(Guid centreId, CancellationToken cancellationToken = default)
    {
        var assignments = await _context.StaffCentreAssignments
            .Find(a => a.CentreId == centreId && a.EndDate == null)
            .ToListAsync(cancellationToken);

        var staffIds = assignments.Select(a => a.StaffMemberId).ToList();
        
        return await _context.StaffMembers
            .Find(s => staffIds.Contains(s.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<StaffMember> AddAsync(StaffMember staffMember, CancellationToken cancellationToken = default)
    {
        await _context.StaffMembers.InsertOneAsync(staffMember, cancellationToken: cancellationToken);
        return staffMember;
    }

    public async Task UpdateAsync(StaffMember staffMember, CancellationToken cancellationToken = default)
    {
        await _context.StaffMembers.ReplaceOneAsync(
            s => s.Id == staffMember.Id,
            staffMember,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.StaffMembers.DeleteOneAsync(
            s => s.Id == id,
            cancellationToken);
    }

    public async Task<List<StaffCentreAssignment>> GetCentreAssignmentsAsync(Guid staffMemberId, CancellationToken cancellationToken = default)
    {
        return await _context.StaffCentreAssignments
            .Find(a => a.StaffMemberId == staffMemberId)
            .ToListAsync(cancellationToken);
    }

    public async Task<StaffCentreAssignment> AddCentreAssignmentAsync(StaffCentreAssignment assignment, CancellationToken cancellationToken = default)
    {
        await _context.StaffCentreAssignments.InsertOneAsync(assignment, cancellationToken: cancellationToken);
        return assignment;
    }

    public async Task RemoveCentreAssignmentAsync(Guid staffMemberId, Guid centreId, CancellationToken cancellationToken = default)
    {
        await _context.StaffCentreAssignments.DeleteOneAsync(
            a => a.StaffMemberId == staffMemberId && a.CentreId == centreId,
            cancellationToken);
    }
}
