using MongoDB.Driver;
using SchoolConnect.Identity.Domain.Entities;
using SchoolConnect.Identity.Domain.Interfaces;
using SchoolConnect.Identity.Infrastructure.Persistence;

namespace SchoolConnect.Identity.Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly IdentityDbContext _context;

    public SessionRepository(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Session?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sessions.Find(s => s.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Session>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Sessions.Find(s => s.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<List<Session>> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Sessions.Find(s => s.UserId == userId && s.EndedAt == null).ToListAsync(cancellationToken);
    }

    public async Task<Session> AddAsync(Session session, CancellationToken cancellationToken = default)
    {
        await _context.Sessions.InsertOneAsync(session, cancellationToken: cancellationToken);
        return session;
    }

    public async Task UpdateAsync(Session session, CancellationToken cancellationToken = default)
    {
        await _context.Sessions.ReplaceOneAsync(s => s.Id == session.Id, session, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Sessions.DeleteOneAsync(s => s.Id == id, cancellationToken);
    }
}
