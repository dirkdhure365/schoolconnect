using MongoDB.Driver;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Interfaces;
using SchoolConnect.Institution.Infrastructure.Persistence;

namespace SchoolConnect.Institution.Infrastructure.Repositories;

public class CentreRepository : ICentreRepository
{
    private readonly InstitutionDbContext _context;

    public CentreRepository(InstitutionDbContext context)
    {
        _context = context;
    }

    public async Task<Centre?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Centres
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Centre?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _context.Centres
            .Find(c => c.Code == code)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Centre>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _context.Centres
            .Find(c => c.InstituteId == instituteId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Centre> AddAsync(Centre centre, CancellationToken cancellationToken = default)
    {
        await _context.Centres.InsertOneAsync(centre, cancellationToken: cancellationToken);
        return centre;
    }

    public async Task UpdateAsync(Centre centre, CancellationToken cancellationToken = default)
    {
        await _context.Centres.ReplaceOneAsync(
            c => c.Id == centre.Id,
            centre,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Centres.DeleteOneAsync(
            c => c.Id == id,
            cancellationToken);
    }
}
