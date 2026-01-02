using SchoolConnect.LessonDelivery.Domain.Entities;

namespace SchoolConnect.LessonDelivery.Domain.Interfaces;

public interface IScheduledLessonRepository
{
    Task<ScheduledLesson?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ScheduledLesson>> GetByClassIdAsync(Guid classId, CancellationToken cancellationToken = default);
    Task<List<ScheduledLesson>> GetByTeacherIdAsync(Guid teacherId, CancellationToken cancellationToken = default);
    Task<List<ScheduledLesson>> GetUpcomingAsync(DateTime fromDate, CancellationToken cancellationToken = default);
    Task AddAsync(ScheduledLesson lesson, CancellationToken cancellationToken = default);
    Task UpdateAsync(ScheduledLesson lesson, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
