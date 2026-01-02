using SchoolConnect.LessonDelivery.Domain.Entities;

namespace SchoolConnect.LessonDelivery.Domain.Interfaces;

public interface ILessonTemplateRepository
{
    Task<LessonTemplate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<LessonTemplate>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default);
    Task AddAsync(LessonTemplate template, CancellationToken cancellationToken = default);
    Task UpdateAsync(LessonTemplate template, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
