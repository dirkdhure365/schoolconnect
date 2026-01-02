using SchoolConnect.Institution.Domain.Entities;

namespace SchoolConnect.Institution.Domain.Interfaces;

public interface IInstituteRepository
{
    Task<Institute?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Institute?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<List<Institute>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Institute> AddAsync(Institute institute, CancellationToken cancellationToken = default);
    Task UpdateAsync(Institute institute, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
