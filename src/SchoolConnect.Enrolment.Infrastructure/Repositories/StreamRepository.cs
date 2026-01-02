using MongoDB.Driver;
using StreamEntity = SchoolConnect.Enrolment.Domain.Entities.Stream;
using SchoolConnect.Enrolment.Domain.Interfaces;
using SchoolConnect.Enrolment.Infrastructure.Persistence;

namespace SchoolConnect.Enrolment.Infrastructure.Repositories;

public class StreamRepository : IStreamRepository
{
    private readonly EnrolmentDbContext _context;

    public StreamRepository(EnrolmentDbContext context)
    {
        _context = context;
    }

    public async Task<StreamEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Streams
            .Find(s => s.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<StreamEntity>> GetByInstituteAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _context.Streams
            .Find(s => s.InstituteId == instituteId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(StreamEntity stream, CancellationToken cancellationToken = default)
    {
        await _context.Streams.InsertOneAsync(stream, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(StreamEntity stream, CancellationToken cancellationToken = default)
    {
        await _context.Streams.ReplaceOneAsync(
            s => s.Id == stream.Id,
            stream,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Streams.DeleteOneAsync(
            s => s.Id == id,
            cancellationToken);
    }
}
