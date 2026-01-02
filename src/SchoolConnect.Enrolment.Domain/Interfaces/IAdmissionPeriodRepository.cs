using SchoolConnect.Enrolment.Domain.Entities;

namespace SchoolConnect.Enrolment.Domain.Interfaces;

public interface IAdmissionPeriodRepository
{
    Task<AdmissionPeriod?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<AdmissionPeriod>> GetByInstituteAsync(Guid instituteId, CancellationToken cancellationToken = default);
    Task AddAsync(AdmissionPeriod admissionPeriod, CancellationToken cancellationToken = default);
    Task UpdateAsync(AdmissionPeriod admissionPeriod, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
