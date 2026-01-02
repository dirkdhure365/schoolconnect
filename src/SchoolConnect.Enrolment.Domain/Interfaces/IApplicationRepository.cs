using SchoolConnect.Enrolment.Domain.Entities;

namespace SchoolConnect.Enrolment.Domain.Interfaces;

public interface IApplicationRepository
{
    Task<Application?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Application?> GetByApplicationNumberAsync(string applicationNumber, CancellationToken cancellationToken = default);
    Task<IEnumerable<Application>> GetByAdmissionPeriodAsync(Guid admissionPeriodId, CancellationToken cancellationToken = default);
    Task AddAsync(Application application, CancellationToken cancellationToken = default);
    Task UpdateAsync(Application application, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
