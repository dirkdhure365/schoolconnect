using SchoolConnect.LessonDelivery.Domain.Entities;

namespace SchoolConnect.LessonDelivery.Domain.Interfaces;

public interface ICurriculumCoverageRepository
{
    Task<CurriculumCoverage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<CurriculumCoverage>> GetByClassIdAsync(Guid classId, CancellationToken cancellationToken = default);
    Task<CurriculumCoverage?> GetByTopicIdAsync(Guid classId, Guid topicId, CancellationToken cancellationToken = default);
    Task AddAsync(CurriculumCoverage coverage, CancellationToken cancellationToken = default);
    Task UpdateAsync(CurriculumCoverage coverage, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
