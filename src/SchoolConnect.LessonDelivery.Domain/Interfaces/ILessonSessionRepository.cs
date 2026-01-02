using SchoolConnect.LessonDelivery.Domain.Entities;

namespace SchoolConnect.LessonDelivery.Domain.Interfaces;

public interface ILessonSessionRepository
{
    Task<LessonSession?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<LessonSession>> GetByClassIdAsync(Guid classId, CancellationToken cancellationToken = default);
    Task<List<LessonSession>> GetByScheduledLessonIdAsync(Guid scheduledLessonId, CancellationToken cancellationToken = default);
    Task AddAsync(LessonSession session, CancellationToken cancellationToken = default);
    Task UpdateAsync(LessonSession session, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
