using MongoDB.Driver;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Interfaces;
using SchoolConnect.Institution.Infrastructure.Persistence;

namespace SchoolConnect.Institution.Infrastructure.Repositories;

public class InstituteRepository : IInstituteRepository
{
    private readonly InstitutionDbContext _context;

    public InstituteRepository(InstitutionDbContext context)
    {
        _context = context;
    }

    public async Task<Institute?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Institutes
            .Find(i => i.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Institute?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _context.Institutes
            .Find(i => i.Code == code)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Institute>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Institutes
            .Find(_ => true)
            .ToListAsync(cancellationToken);
    }

    public async Task<Institute> AddAsync(Institute institute, CancellationToken cancellationToken = default)
    {
        await _context.Institutes.InsertOneAsync(institute, cancellationToken: cancellationToken);
        return institute;
    }

    public async Task UpdateAsync(Institute institute, CancellationToken cancellationToken = default)
    {
        await _context.Institutes.ReplaceOneAsync(
            i => i.Id == institute.Id,
            institute,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Institutes.DeleteOneAsync(
            i => i.Id == id,
            cancellationToken);
    }
}
