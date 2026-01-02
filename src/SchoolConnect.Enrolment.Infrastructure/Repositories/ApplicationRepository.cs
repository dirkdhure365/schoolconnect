using MongoDB.Driver;
using ApplicationEntity = SchoolConnect.Enrolment.Domain.Entities.Application;
using SchoolConnect.Enrolment.Domain.Interfaces;
using SchoolConnect.Enrolment.Infrastructure.Persistence;

namespace SchoolConnect.Enrolment.Infrastructure.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly EnrolmentDbContext _context;

    public ApplicationRepository(EnrolmentDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Applications
            .Find(a => a.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ApplicationEntity?> GetByApplicationNumberAsync(string applicationNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Applications
            .Find(a => a.ApplicationNumber == applicationNumber)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<ApplicationEntity>> GetByAdmissionPeriodAsync(Guid admissionPeriodId, CancellationToken cancellationToken = default)
    {
        return await _context.Applications
            .Find(a => a.AdmissionPeriodId == admissionPeriodId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ApplicationEntity application, CancellationToken cancellationToken = default)
    {
        await _context.Applications.InsertOneAsync(application, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(ApplicationEntity application, CancellationToken cancellationToken = default)
    {
        await _context.Applications.ReplaceOneAsync(
            a => a.Id == application.Id,
            application,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Applications.DeleteOneAsync(
            a => a.Id == id,
            cancellationToken);
    }
}
