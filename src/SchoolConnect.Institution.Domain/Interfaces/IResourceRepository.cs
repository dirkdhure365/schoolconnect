using SchoolConnect.Institution.Domain.Entities;

namespace SchoolConnect.Institution.Domain.Interfaces;

public interface IResourceRepository
{
    Task<Resource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Resource>> GetByCentreIdAsync(Guid centreId, CancellationToken cancellationToken = default);
    Task<Resource> AddAsync(Resource resource, CancellationToken cancellationToken = default);
    Task UpdateAsync(Resource resource, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ResourceAllocation?> GetAllocationByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ResourceAllocation>> GetAllocationsByResourceIdAsync(Guid resourceId, CancellationToken cancellationToken = default);
    Task<ResourceAllocation> AddAllocationAsync(ResourceAllocation allocation, CancellationToken cancellationToken = default);
    Task UpdateAllocationAsync(ResourceAllocation allocation, CancellationToken cancellationToken = default);
}
