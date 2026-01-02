using MongoDB.Driver;
using SchoolConnect.LessonDelivery.Domain.Entities;
using SchoolConnect.LessonDelivery.Domain.Interfaces;
using SchoolConnect.LessonDelivery.Infrastructure.Persistence;

namespace SchoolConnect.LessonDelivery.Infrastructure.Repositories;

public class HomeworkRepository : IHomeworkRepository
{
    private readonly LessonDeliveryDbContext _context;

    public HomeworkRepository(LessonDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<Homework?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Homeworks
            .Find(h => h.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Homework>> GetByClassIdAsync(Guid classId, CancellationToken cancellationToken = default)
    {
        return await _context.Homeworks
            .Find(h => h.ClassId == classId)
            .ToListAsync(cancellationToken);
    }

    public async Task<HomeworkSubmission?> GetSubmissionByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.HomeworkSubmissions
            .Find(hs => hs.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<HomeworkSubmission>> GetSubmissionsByHomeworkIdAsync(Guid homeworkId, CancellationToken cancellationToken = default)
    {
        return await _context.HomeworkSubmissions
            .Find(hs => hs.HomeworkId == homeworkId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<HomeworkSubmission>> GetSubmissionsByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default)
    {
        return await _context.HomeworkSubmissions
            .Find(hs => hs.StudentId == studentId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Homework homework, CancellationToken cancellationToken = default)
    {
        await _context.Homeworks.InsertOneAsync(homework, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Homework homework, CancellationToken cancellationToken = default)
    {
        await _context.Homeworks.ReplaceOneAsync(
            h => h.Id == homework.Id,
            homework,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Homeworks.DeleteOneAsync(h => h.Id == id, cancellationToken);
    }

    public async Task AddSubmissionAsync(HomeworkSubmission submission, CancellationToken cancellationToken = default)
    {
        await _context.HomeworkSubmissions.InsertOneAsync(submission, cancellationToken: cancellationToken);
    }

    public async Task UpdateSubmissionAsync(HomeworkSubmission submission, CancellationToken cancellationToken = default)
    {
        await _context.HomeworkSubmissions.ReplaceOneAsync(
            hs => hs.Id == submission.Id,
            submission,
            cancellationToken: cancellationToken);
    }
}
