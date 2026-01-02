using MongoDB.Driver;
using SchoolConnect.Collaboration.Domain.Entities;
using SchoolConnect.Collaboration.Domain.Interfaces;
using SchoolConnect.Collaboration.Infrastructure.Persistence;

namespace SchoolConnect.Collaboration.Infrastructure.Repositories;

public class ListRepository : IListRepository
{
    private readonly CollaborationDbContext _context;

    public ListRepository(CollaborationDbContext context)
    {
        _context = context;
    }

    public async Task<BoardList?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Lists
            .Find(l => l.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<BoardList>> GetByBoardIdAsync(Guid boardId, CancellationToken cancellationToken = default)
    {
        return await _context.Lists
            .Find(l => l.BoardId == boardId && !l.IsArchived)
            .SortBy(l => l.Position)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<BoardList>> GetArchivedByBoardIdAsync(Guid boardId, CancellationToken cancellationToken = default)
    {
        return await _context.Lists
            .Find(l => l.BoardId == boardId && l.IsArchived)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BoardList list, CancellationToken cancellationToken = default)
    {
        await _context.Lists.InsertOneAsync(list, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(BoardList list, CancellationToken cancellationToken = default)
    {
        await _context.Lists.ReplaceOneAsync(
            l => l.Id == list.Id,
            list,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Lists.DeleteOneAsync(l => l.Id == id, cancellationToken);
    }
}
