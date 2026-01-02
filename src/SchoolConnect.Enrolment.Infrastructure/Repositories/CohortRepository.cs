using MongoDB.Driver;
using SchoolConnect.Enrolment.Domain.Entities;
using SchoolConnect.Enrolment.Domain.Interfaces;
using SchoolConnect.Enrolment.Infrastructure.Persistence;

namespace SchoolConnect.Enrolment.Infrastructure.Repositories;

public class CohortRepository : ICohortRepository
{
    private readonly EnrolmentDbContext _context;

    public CohortRepository(EnrolmentDbContext context)
    {
        _context = context;
    }

    public async Task<Cohort?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Cohorts
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Cohort>> GetByStreamAsync(Guid streamId, CancellationToken cancellationToken = default)
    {
        return await _context.Cohorts
            .Find(c => c.StreamId == streamId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Cohort cohort, CancellationToken cancellationToken = default)
    {
        await _context.Cohorts.InsertOneAsync(cohort, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Cohort cohort, CancellationToken cancellationToken = default)
    {
        await _context.Cohorts.ReplaceOneAsync(
            c => c.Id == cohort.Id,
            cohort,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Cohorts.DeleteOneAsync(
            c => c.Id == id,
            cancellationToken);
    }
}
