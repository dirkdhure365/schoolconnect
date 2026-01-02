using MongoDB.Driver;
using SchoolConnect.LessonDelivery.Domain.Entities;
using SchoolConnect.LessonDelivery.Domain.Interfaces;
using SchoolConnect.LessonDelivery.Infrastructure.Persistence;

namespace SchoolConnect.LessonDelivery.Infrastructure.Repositories;

public class LessonPlanRepository : ILessonPlanRepository
{
    private readonly LessonDeliveryDbContext _context;

    public LessonPlanRepository(LessonDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<LessonPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.LessonPlans
            .Find(lp => lp.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<LessonPlan>> GetByClassIdAsync(Guid classId, CancellationToken cancellationToken = default)
    {
        return await _context.LessonPlans
            .Find(lp => lp.ClassId == classId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<LessonPlan>> GetByTeacherIdAsync(Guid teacherId, CancellationToken cancellationToken = default)
    {
        return await _context.LessonPlans
            .Find(lp => lp.TeacherId == teacherId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(LessonPlan lessonPlan, CancellationToken cancellationToken = default)
    {
        await _context.LessonPlans.InsertOneAsync(lessonPlan, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(LessonPlan lessonPlan, CancellationToken cancellationToken = default)
    {
        await _context.LessonPlans.ReplaceOneAsync(
            lp => lp.Id == lessonPlan.Id,
            lessonPlan,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.LessonPlans.DeleteOneAsync(lp => lp.Id == id, cancellationToken);
    }
}
