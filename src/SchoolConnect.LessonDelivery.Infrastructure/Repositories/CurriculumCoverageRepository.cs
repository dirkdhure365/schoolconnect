using MongoDB.Driver;
using SchoolConnect.LessonDelivery.Domain.Entities;
using SchoolConnect.LessonDelivery.Domain.Interfaces;
using SchoolConnect.LessonDelivery.Infrastructure.Persistence;

namespace SchoolConnect.LessonDelivery.Infrastructure.Repositories;

public class CurriculumCoverageRepository : ICurriculumCoverageRepository
{
    private readonly LessonDeliveryDbContext _context;

    public CurriculumCoverageRepository(LessonDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<CurriculumCoverage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.CurriculumCoverages
            .Find(cc => cc.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<CurriculumCoverage>> GetByClassIdAsync(Guid classId, CancellationToken cancellationToken = default)
    {
        return await _context.CurriculumCoverages
            .Find(cc => cc.ClassId == classId)
            .ToListAsync(cancellationToken);
    }

    public async Task<CurriculumCoverage?> GetByTopicIdAsync(Guid classId, Guid topicId, CancellationToken cancellationToken = default)
    {
        return await _context.CurriculumCoverages
            .Find(cc => cc.ClassId == classId && cc.CurriculumTopicId == topicId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(CurriculumCoverage coverage, CancellationToken cancellationToken = default)
    {
        await _context.CurriculumCoverages.InsertOneAsync(coverage, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(CurriculumCoverage coverage, CancellationToken cancellationToken = default)
    {
        await _context.CurriculumCoverages.ReplaceOneAsync(
            cc => cc.Id == coverage.Id,
            coverage,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.CurriculumCoverages.DeleteOneAsync(cc => cc.Id == id, cancellationToken);
    }
}
