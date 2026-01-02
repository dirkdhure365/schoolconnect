using ApplicationEntity = SchoolConnect.Enrolment.Domain.Entities.Application;

namespace SchoolConnect.Enrolment.Domain.Interfaces;

public interface IApplicationRepository
{
    Task<ApplicationEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApplicationEntity?> GetByApplicationNumberAsync(string applicationNumber, CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationEntity>> GetByAdmissionPeriodAsync(Guid admissionPeriodId, CancellationToken cancellationToken = default);
    Task AddAsync(ApplicationEntity application, CancellationToken cancellationToken = default);
    Task UpdateAsync(ApplicationEntity application, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
