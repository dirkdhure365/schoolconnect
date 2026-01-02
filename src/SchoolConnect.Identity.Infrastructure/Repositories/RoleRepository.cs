using MongoDB.Driver;
using SchoolConnect.Identity.Domain.Entities;
using SchoolConnect.Identity.Domain.Interfaces;
using SchoolConnect.Identity.Infrastructure.Persistence;

namespace SchoolConnect.Identity.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly IdentityDbContext _context;

    public RoleRepository(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Roles.Find(r => r.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Roles.Find(r => r.Name == name).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Role>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Roles.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<List<Role>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Roles.Find(r => ids.Contains(r.Id)).ToListAsync(cancellationToken);
    }

    public async Task<Role> AddAsync(Role role, CancellationToken cancellationToken = default)
    {
        await _context.Roles.InsertOneAsync(role, cancellationToken: cancellationToken);
        return role;
    }

    public async Task UpdateAsync(Role role, CancellationToken cancellationToken = default)
    {
        await _context.Roles.ReplaceOneAsync(r => r.Id == role.Id, role, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Roles.DeleteOneAsync(r => r.Id == id, cancellationToken);
    }
}
