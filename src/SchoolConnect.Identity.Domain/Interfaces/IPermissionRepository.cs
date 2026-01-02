using SchoolConnect.Identity.Domain.Entities;

namespace SchoolConnect.Identity.Domain.Interfaces;

public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Permission?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<Permission>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default);
    Task<Permission> AddAsync(Permission permission, CancellationToken cancellationToken = default);
}
