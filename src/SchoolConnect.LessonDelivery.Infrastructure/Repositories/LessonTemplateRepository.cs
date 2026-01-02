using MongoDB.Driver;
using SchoolConnect.LessonDelivery.Domain.Entities;
using SchoolConnect.LessonDelivery.Domain.Interfaces;
using SchoolConnect.LessonDelivery.Infrastructure.Persistence;

namespace SchoolConnect.LessonDelivery.Infrastructure.Repositories;

public class LessonTemplateRepository : ILessonTemplateRepository
{
    private readonly LessonDeliveryDbContext _context;

    public LessonTemplateRepository(LessonDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<LessonTemplate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.LessonTemplates
            .Find(lt => lt.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<LessonTemplate>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _context.LessonTemplates
            .Find(lt => lt.InstituteId == instituteId && lt.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(LessonTemplate template, CancellationToken cancellationToken = default)
    {
        await _context.LessonTemplates.InsertOneAsync(template, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(LessonTemplate template, CancellationToken cancellationToken = default)
    {
        await _context.LessonTemplates.ReplaceOneAsync(
            lt => lt.Id == template.Id,
            template,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.LessonTemplates.DeleteOneAsync(lt => lt.Id == id, cancellationToken);
    }
}
