using MongoDB.Driver;
using SchoolConnect.Collaboration.Domain.Entities;
using SchoolConnect.Collaboration.Domain.Interfaces;
using SchoolConnect.Collaboration.Infrastructure.Persistence;

namespace SchoolConnect.Collaboration.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly CollaborationDbContext _context;

    public CardRepository(CollaborationDbContext context)
    {
        _context = context;
    }

    public async Task<Card?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Cards
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Card>> GetByListIdAsync(Guid listId, CancellationToken cancellationToken = default)
    {
        return await _context.Cards
            .Find(c => c.ListId == listId && !c.IsArchived)
            .SortBy(c => c.Position)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Card>> GetByBoardIdAsync(Guid boardId, CancellationToken cancellationToken = default)
    {
        return await _context.Cards
            .Find(c => c.BoardId == boardId && !c.IsArchived)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Card>> GetArchivedByBoardIdAsync(Guid boardId, CancellationToken cancellationToken = default)
    {
        return await _context.Cards
            .Find(c => c.BoardId == boardId && c.IsArchived)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Card>> GetByAssigneeIdAsync(Guid assigneeId, CancellationToken cancellationToken = default)
    {
        return await _context.Cards
            .Find(c => c.AssigneeIds.Contains(assigneeId) && !c.IsArchived)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Card>> GetDueSoonAsync(DateTime before, CancellationToken cancellationToken = default)
    {
        return await _context.Cards
            .Find(c => c.DueDate != null && c.DueDate <= before && !c.IsDueComplete && !c.IsArchived)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Card>> SearchAsync(Guid boardId, string searchTerm, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Card>.Filter.And(
            Builders<Card>.Filter.Eq(c => c.BoardId, boardId),
            Builders<Card>.Filter.Or(
                Builders<Card>.Filter.Regex(c => c.Title, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
                Builders<Card>.Filter.Regex(c => c.Description, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"))
            )
        );

        return await _context.Cards
            .Find(filter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Card card, CancellationToken cancellationToken = default)
    {
        await _context.Cards.InsertOneAsync(card, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Card card, CancellationToken cancellationToken = default)
    {
        await _context.Cards.ReplaceOneAsync(
            c => c.Id == card.Id,
            card,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Cards.DeleteOneAsync(c => c.Id == id, cancellationToken);
    }
}
