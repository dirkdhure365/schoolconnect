using SchoolConnect.LessonDelivery.Domain.Entities;

namespace SchoolConnect.LessonDelivery.Domain.Interfaces;

public interface ILessonPlanRepository
{
    Task<LessonPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<LessonPlan>> GetByClassIdAsync(Guid classId, CancellationToken cancellationToken = default);
    Task<List<LessonPlan>> GetByTeacherIdAsync(Guid teacherId, CancellationToken cancellationToken = default);
    Task AddAsync(LessonPlan lessonPlan, CancellationToken cancellationToken = default);
    Task UpdateAsync(LessonPlan lessonPlan, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
