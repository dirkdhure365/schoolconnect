using SchoolConnect.Institution.Domain.Entities;

namespace SchoolConnect.Institution.Domain.Interfaces;

public interface ICentreRepository
{
    Task<Centre?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Centre?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<List<Centre>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default);
    Task<Centre> AddAsync(Centre centre, CancellationToken cancellationToken = default);
    Task UpdateAsync(Centre centre, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
