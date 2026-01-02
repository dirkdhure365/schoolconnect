using SchoolConnect.LessonDelivery.Domain.Entities;

namespace SchoolConnect.LessonDelivery.Domain.Interfaces;

public interface IHomeworkRepository
{
    Task<Homework?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Homework>> GetByClassIdAsync(Guid classId, CancellationToken cancellationToken = default);
    Task<HomeworkSubmission?> GetSubmissionByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<HomeworkSubmission>> GetSubmissionsByHomeworkIdAsync(Guid homeworkId, CancellationToken cancellationToken = default);
    Task<List<HomeworkSubmission>> GetSubmissionsByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default);
    Task AddAsync(Homework homework, CancellationToken cancellationToken = default);
    Task UpdateAsync(Homework homework, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddSubmissionAsync(HomeworkSubmission submission, CancellationToken cancellationToken = default);
    Task UpdateSubmissionAsync(HomeworkSubmission submission, CancellationToken cancellationToken = default);
}
