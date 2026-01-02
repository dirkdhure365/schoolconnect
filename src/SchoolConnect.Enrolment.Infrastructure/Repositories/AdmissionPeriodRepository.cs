using MongoDB.Driver;
using SchoolConnect.Enrolment.Domain.Entities;
using SchoolConnect.Enrolment.Domain.Interfaces;
using SchoolConnect.Enrolment.Infrastructure.Persistence;

namespace SchoolConnect.Enrolment.Infrastructure.Repositories;

public class AdmissionPeriodRepository : IAdmissionPeriodRepository
{
    private readonly EnrolmentDbContext _context;

    public AdmissionPeriodRepository(EnrolmentDbContext context)
    {
        _context = context;
    }

    public async Task<AdmissionPeriod?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.AdmissionPeriods
            .Find(ap => ap.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<AdmissionPeriod>> GetByInstituteAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _context.AdmissionPeriods
            .Find(ap => ap.InstituteId == instituteId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AdmissionPeriod admissionPeriod, CancellationToken cancellationToken = default)
    {
        await _context.AdmissionPeriods.InsertOneAsync(admissionPeriod, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(AdmissionPeriod admissionPeriod, CancellationToken cancellationToken = default)
    {
        await _context.AdmissionPeriods.ReplaceOneAsync(
            ap => ap.Id == admissionPeriod.Id,
            admissionPeriod,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.AdmissionPeriods.DeleteOneAsync(
            ap => ap.Id == id,
            cancellationToken);
    }
}
