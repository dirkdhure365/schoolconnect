using MongoDB.Driver;
using SchoolConnect.Collaboration.Domain.Entities;
using SchoolConnect.Collaboration.Domain.Interfaces;
using SchoolConnect.Collaboration.Infrastructure.Persistence;

namespace SchoolConnect.Collaboration.Infrastructure.Repositories;

public class BoardRepository : IBoardRepository
{
    private readonly CollaborationDbContext _context;

    public BoardRepository(CollaborationDbContext context)
    {
        _context = context;
    }

    public async Task<Board?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Boards
            .Find(b => b.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Board>> GetByWorkspaceIdAsync(Guid workspaceId, CancellationToken cancellationToken = default)
    {
        return await _context.Boards
            .Find(b => b.WorkspaceId == workspaceId && !b.IsArchived)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Board>> GetArchivedByWorkspaceIdAsync(Guid workspaceId, CancellationToken cancellationToken = default)
    {
        return await _context.Boards
            .Find(b => b.WorkspaceId == workspaceId && b.IsArchived)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Board board, CancellationToken cancellationToken = default)
    {
        await _context.Boards.InsertOneAsync(board, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Board board, CancellationToken cancellationToken = default)
    {
        await _context.Boards.ReplaceOneAsync(
            b => b.Id == board.Id,
            board,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Boards.DeleteOneAsync(b => b.Id == id, cancellationToken);
    }
}
