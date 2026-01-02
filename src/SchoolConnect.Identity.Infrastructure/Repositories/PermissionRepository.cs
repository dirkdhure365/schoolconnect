using MongoDB.Driver;
using SchoolConnect.Identity.Domain.Entities;
using SchoolConnect.Identity.Domain.Interfaces;
using SchoolConnect.Identity.Infrastructure.Persistence;

namespace SchoolConnect.Identity.Infrastructure.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly IdentityDbContext _context;

    public PermissionRepository(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Permission?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Permissions.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Permission?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _context.Permissions.Find(p => p.Code == code).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Permissions.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<List<Permission>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Permissions.Find(p => ids.Contains(p.Id)).ToListAsync(cancellationToken);
    }

    public async Task<Permission> AddAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        await _context.Permissions.InsertOneAsync(permission, cancellationToken: cancellationToken);
        return permission;
    }
}
