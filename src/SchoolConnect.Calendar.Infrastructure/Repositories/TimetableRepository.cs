using MongoDB.Driver;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.Interfaces;
using SchoolConnect.Calendar.Infrastructure.Persistence;

namespace SchoolConnect.Calendar.Infrastructure.Repositories;

public class TimetableRepository : ITimetableRepository
{
    private readonly CalendarDbContext _context;

    public TimetableRepository(CalendarDbContext context)
    {
        _context = context;
    }

    public async Task<Timetable?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Timetables
            .Find(t => t.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Timetable>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _context.Timetables
            .Find(t => t.InstituteId == instituteId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Timetable>> GetByCentreIdAsync(Guid centreId, CancellationToken cancellationToken = default)
    {
        return await _context.Timetables
            .Find(t => t.CentreId == centreId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Timetable timetable, CancellationToken cancellationToken = default)
    {
        await _context.Timetables.InsertOneAsync(timetable, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Timetable timetable, CancellationToken cancellationToken = default)
    {
        await _context.Timetables.ReplaceOneAsync(
            t => t.Id == timetable.Id,
            timetable,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Timetables.DeleteOneAsync(t => t.Id == id, cancellationToken);
    }
}
