using MongoDB.Driver;
using SchoolConnect.LessonDelivery.Domain.Entities;
using SchoolConnect.LessonDelivery.Domain.Interfaces;
using SchoolConnect.LessonDelivery.Infrastructure.Persistence;

namespace SchoolConnect.LessonDelivery.Infrastructure.Repositories;

public class LessonSessionRepository : ILessonSessionRepository
{
    private readonly LessonDeliveryDbContext _context;

    public LessonSessionRepository(LessonDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<LessonSession?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.LessonSessions
            .Find(ls => ls.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<LessonSession>> GetByClassIdAsync(Guid classId, CancellationToken cancellationToken = default)
    {
        return await _context.LessonSessions
            .Find(ls => ls.ClassId == classId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<LessonSession>> GetByScheduledLessonIdAsync(Guid scheduledLessonId, CancellationToken cancellationToken = default)
    {
        return await _context.LessonSessions
            .Find(ls => ls.ScheduledLessonId == scheduledLessonId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(LessonSession session, CancellationToken cancellationToken = default)
    {
        await _context.LessonSessions.InsertOneAsync(session, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(LessonSession session, CancellationToken cancellationToken = default)
    {
        await _context.LessonSessions.ReplaceOneAsync(
            ls => ls.Id == session.Id,
            session,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.LessonSessions.DeleteOneAsync(ls => ls.Id == id, cancellationToken);
    }
}
