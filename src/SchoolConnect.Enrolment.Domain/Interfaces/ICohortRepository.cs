using SchoolConnect.Enrolment.Domain.Entities;

namespace SchoolConnect.Enrolment.Domain.Interfaces;

public interface ICohortRepository
{
    Task<Cohort?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Cohort>> GetByStreamAsync(Guid streamId, CancellationToken cancellationToken = default);
    Task AddAsync(Cohort cohort, CancellationToken cancellationToken = default);
    Task UpdateAsync(Cohort cohort, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
