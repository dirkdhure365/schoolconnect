using MongoDB.Driver;
using SchoolConnect.Enrolment.Domain.Entities;
using SchoolConnect.Enrolment.Domain.Interfaces;
using SchoolConnect.Enrolment.Infrastructure.Persistence;

namespace SchoolConnect.Enrolment.Infrastructure.Repositories;

public class ClassRepository : IClassRepository
{
    private readonly EnrolmentDbContext _context;

    public ClassRepository(EnrolmentDbContext context)
    {
        _context = context;
    }

    public async Task<Class?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Classes
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Class>> GetByCohortAsync(Guid cohortId, CancellationToken cancellationToken = default)
    {
        return await _context.Classes
            .Find(c => c.CohortId == cohortId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Class>> GetByTeacherAsync(Guid teacherId, CancellationToken cancellationToken = default)
    {
        return await _context.Classes
            .Find(c => c.TeacherId == teacherId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Class @class, CancellationToken cancellationToken = default)
    {
        await _context.Classes.InsertOneAsync(@class, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Class @class, CancellationToken cancellationToken = default)
    {
        await _context.Classes.ReplaceOneAsync(
            c => c.Id == @class.Id,
            @class,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Classes.DeleteOneAsync(
            c => c.Id == id,
            cancellationToken);
    }
}
