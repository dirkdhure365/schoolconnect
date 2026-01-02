using MongoDB.Driver;
using SchoolConnect.LessonDelivery.Domain.Entities;
using SchoolConnect.LessonDelivery.Domain.Interfaces;
using SchoolConnect.LessonDelivery.Infrastructure.Persistence;

namespace SchoolConnect.LessonDelivery.Infrastructure.Repositories;

public class ScheduledLessonRepository : IScheduledLessonRepository
{
    private readonly LessonDeliveryDbContext _context;

    public ScheduledLessonRepository(LessonDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<ScheduledLesson?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ScheduledLessons
            .Find(sl => sl.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<ScheduledLesson>> GetByClassIdAsync(Guid classId, CancellationToken cancellationToken = default)
    {
        return await _context.ScheduledLessons
            .Find(sl => sl.ClassId == classId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<ScheduledLesson>> GetByTeacherIdAsync(Guid teacherId, CancellationToken cancellationToken = default)
    {
        return await _context.ScheduledLessons
            .Find(sl => sl.TeacherId == teacherId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<ScheduledLesson>> GetUpcomingAsync(DateTime fromDate, CancellationToken cancellationToken = default)
    {
        return await _context.ScheduledLessons
            .Find(sl => sl.ScheduledStart >= fromDate)
            .SortBy(sl => sl.ScheduledStart)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ScheduledLesson lesson, CancellationToken cancellationToken = default)
    {
        await _context.ScheduledLessons.InsertOneAsync(lesson, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(ScheduledLesson lesson, CancellationToken cancellationToken = default)
    {
        await _context.ScheduledLessons.ReplaceOneAsync(
            sl => sl.Id == lesson.Id,
            lesson,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.ScheduledLessons.DeleteOneAsync(sl => sl.Id == id, cancellationToken);
    }
}
