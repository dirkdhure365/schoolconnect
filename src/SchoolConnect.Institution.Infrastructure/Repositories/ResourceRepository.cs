using MongoDB.Driver;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Interfaces;
using SchoolConnect.Institution.Infrastructure.Persistence;

namespace SchoolConnect.Institution.Infrastructure.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly InstitutionDbContext _context;

    public ResourceRepository(InstitutionDbContext context)
    {
        _context = context;
    }

    public async Task<Resource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Resources
            .Find(r => r.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Resource>> GetByCentreIdAsync(Guid centreId, CancellationToken cancellationToken = default)
    {
        return await _context.Resources
            .Find(r => r.CentreId == centreId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Resource> AddAsync(Resource resource, CancellationToken cancellationToken = default)
    {
        await _context.Resources.InsertOneAsync(resource, cancellationToken: cancellationToken);
        return resource;
    }

    public async Task UpdateAsync(Resource resource, CancellationToken cancellationToken = default)
    {
        await _context.Resources.ReplaceOneAsync(
            r => r.Id == resource.Id,
            resource,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Resources.DeleteOneAsync(
            r => r.Id == id,
            cancellationToken);
    }

    public async Task<ResourceAllocation?> GetAllocationByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ResourceAllocations
            .Find(a => a.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<ResourceAllocation>> GetAllocationsByResourceIdAsync(Guid resourceId, CancellationToken cancellationToken = default)
    {
        return await _context.ResourceAllocations
            .Find(a => a.ResourceId == resourceId)
            .ToListAsync(cancellationToken);
    }

    public async Task<ResourceAllocation> AddAllocationAsync(ResourceAllocation allocation, CancellationToken cancellationToken = default)
    {
        await _context.ResourceAllocations.InsertOneAsync(allocation, cancellationToken: cancellationToken);
        return allocation;
    }

    public async Task UpdateAllocationAsync(ResourceAllocation allocation, CancellationToken cancellationToken = default)
    {
        await _context.ResourceAllocations.ReplaceOneAsync(
            a => a.Id == allocation.Id,
            allocation,
            cancellationToken: cancellationToken);
    }
}
